using Floris.Data;
using Floris.Models;
using Floris.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace Floris.Services
{
    /// <summary>Реализация <see cref="ICatalogService"/> на Entity Framework Core.</summary>
    public class CatalogService : ICatalogService
    {
        private readonly FlorisDbContext _db;

        /// <summary>Создаёт сервис.</summary>
        /// <param name="db">Контекст базы данных.</param>
        public CatalogService(FlorisDbContext db)
        {
            _db = db;
        }

        /// <inheritdoc />
        public async Task<IReadOnlyList<CategoryNode>> GetCategoryTreeAsync(CancellationToken ct = default)
        {
            var categories = await _db.Categories.AsNoTracking().OrderBy(c => c.Id).ToListAsync(ct);

            var nodes = categories.ToDictionary(
                c => c.Id,
                c => new CategoryNode { Id = c.Id, Name = c.Name, Slug = c.Slug });

            var roots = new List<CategoryNode>();
            foreach (var category in categories)
            {
                var node = nodes[category.Id];
                if (category.ParentId.HasValue && nodes.TryGetValue(category.ParentId.Value, out var parent))
                {
                    parent.Children.Add(node);
                }
                else
                {
                    roots.Add(node);
                }
            }
            return roots;
        }

        /// <inheritdoc />
        public async Task<IReadOnlyList<CatalogItemViewModel>> GetCatalogAsync(
            int storeId, CatalogFilter filter, CancellationToken ct = default)
        {
            var query = StoreRows(storeId);

            if (!string.IsNullOrEmpty(filter.Category))
            {
                var tree = await GetCategoryTreeAsync(ct);
                var node = CategoryNode.FindBySlug(tree, filter.Category);
                if (node == null)
                {
                    return new List<CatalogItemViewModel>();
                }
                var ids = node.SelfAndDescendantIds().ToList();
                query = query.Where(r => ids.Contains(r.CategoryId));
            }

            if (!string.IsNullOrEmpty(filter.Search))
            {
                var term = filter.Search;
                query = query.Where(r => r.Name.Contains(term)
                                         || (r.Description != null && r.Description.Contains(term)));
            }

            if (filter.InStockOnly)
            {
                query = query.Where(r => r.Quantity > 0);
            }

            var items = (await query.ToListAsync(ct)).Select(ToItem);

            // Сортировка выполняется в памяти: итоговая цена считается из нескольких партий.
            items = filter.Sort switch
            {
                CatalogSort.PriceAsc => items.OrderBy(i => i.Price).ThenBy(i => i.Id),
                CatalogSort.PriceDesc => items.OrderByDescending(i => i.Price).ThenBy(i => i.Id),
                CatalogSort.Name => items.OrderBy(i => i.Name),
                _ => items.OrderBy(i => i.Id)
            };

            return items.ToList();
        }

        /// <inheritdoc />
        public async Task<CatalogItemViewModel?> GetProductAsync(int storeId, int productId, CancellationToken ct = default)
        {
            var row = await StoreRows(storeId).FirstOrDefaultAsync(r => r.Id == productId, ct);
            return row == null ? null : ToItem(row);
        }

        /// <inheritdoc />
        public async Task<IReadOnlyList<CatalogItemViewModel>> GetRelatedAsync(
            int storeId, int productId, int categoryId, int count, CancellationToken ct = default)
        {
            var rows = await StoreRows(storeId)
                .Where(r => r.CategoryId == categoryId && r.Id != productId && r.Quantity > 0)
                .OrderBy(r => r.Id)
                .Take(count)
                .ToListAsync(ct);

            return rows.Select(ToItem).ToList();
        }

        /// <inheritdoc />
        public async Task<IReadOnlyList<CatalogItemViewModel>> GetPopularBouquetsAsync(
            int? storeId, int count, CancellationToken ct = default)
        {
            if (storeId.HasValue)
            {
                var rows = await StoreRows(storeId.Value)
                    .Where(r => r.Type == ProductType.Bouquet)
                    .ToListAsync(ct);

                // Сначала те, что есть в наличии.
                return rows.OrderBy(r => r.Quantity == 0).ThenBy(r => r.Id)
                           .Take(count).Select(ToItem).ToList();
            }

            var products = await _db.Products.AsNoTracking()
                .Where(p => p.IsAvailable && p.Type == ProductType.Bouquet)
                .OrderBy(p => p.Id)
                .Take(count)
                .Select(p => new CatalogItemViewModel
                {
                    Id = p.Id,
                    Name = p.Name,
                    Description = p.Description,
                    PhotoUrl = p.PhotoUrl,
                    CategoryId = p.CategoryId,
                    CategoryName = p.Category.Name,
                    CategorySlug = p.Category.Slug,
                    Type = p.Type,
                    Price = p.BasePrice,
                    Status = StockStatus.Available,
                    HasStoreData = false
                })
                .ToListAsync(ct);

            return products;
        }

        /// <summary>Строит запрос: доступные товары с суммарным остатком и минимальной ценой в магазине.</summary>
        /// <param name="storeId">Идентификатор магазина.</param>
        /// <returns>Запрос строк каталога.</returns>
        private IQueryable<ProductRow> StoreRows(int storeId)
        {
            return _db.Products.AsNoTracking()
                .Where(p => p.IsAvailable)
                .Select(p => new ProductRow
                {
                    Id = p.Id,
                    Name = p.Name,
                    Description = p.Description,
                    PhotoUrl = p.PhotoUrl,
                    CategoryId = p.CategoryId,
                    CategoryName = p.Category.Name,
                    CategorySlug = p.Category.Slug,
                    Type = p.Type,
                    BasePrice = p.BasePrice,
                    Quantity = p.Inventory
                        .Where(i => i.StoreId == storeId)
                        .Sum(i => i.Quantity),
                    MinPrice = p.Inventory
                        .Where(i => i.StoreId == storeId && i.Quantity > 0)
                        .Min(i => (decimal?)i.CurrentPrice),
                    HasMarkdown = p.Inventory
                        .Any(i => i.StoreId == storeId && i.Quantity > 0 && i.Status == StockStatus.Markdown)
                });
        }

        /// <summary>Преобразует строку из базы в модель для страницы.</summary>
        /// <param name="row">Строка каталога.</param>
        /// <returns>Модель товара с вычисленным статусом и ценой.</returns>
        private static CatalogItemViewModel ToItem(ProductRow row)
        {
            var price = row.Quantity > 0 && row.MinPrice.HasValue ? row.MinPrice.Value : row.BasePrice;

            return new CatalogItemViewModel
            {
                Id = row.Id,
                Name = row.Name,
                Description = row.Description,
                PhotoUrl = row.PhotoUrl,
                CategoryId = row.CategoryId,
                CategoryName = row.CategoryName,
                CategorySlug = row.CategorySlug,
                Type = row.Type,
                Price = price,
                OldPrice = price < row.BasePrice ? row.BasePrice : null,
                Quantity = row.Quantity,
                Status = StockStatusResolver.Resolve(row.Quantity, row.HasMarkdown),
                HasStoreData = true
            };
        }

        /// <summary>Промежуточная строка запроса каталога (данные товара и агрегаты по складу).</summary>
        private sealed class ProductRow
        {
            public int Id { get; set; }
            public string Name { get; set; } = string.Empty;
            public string? Description { get; set; }
            public string? PhotoUrl { get; set; }
            public int CategoryId { get; set; }
            public string CategoryName { get; set; } = string.Empty;
            public string CategorySlug { get; set; } = string.Empty;
            public ProductType Type { get; set; }
            public decimal BasePrice { get; set; }
            public int Quantity { get; set; }
            public decimal? MinPrice { get; set; }
            public bool HasMarkdown { get; set; }
        }
    }
}
