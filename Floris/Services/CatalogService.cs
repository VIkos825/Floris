using Floris.Data;
using Floris.Models;
using Microsoft.EntityFrameworkCore;

namespace Floris.Services
{
    /// <summary>Реализация <see cref="ICatalogService"/> на основе Entity Framework Core.</summary>
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
        public async Task<IReadOnlyList<City>> GetCitiesAsync(CancellationToken ct = default)
        {
            return await _db.Cities.AsNoTracking().OrderBy(c => c.Name).ToListAsync(ct);
        }

        /// <inheritdoc />
        public async Task<IReadOnlyList<Product>> GetPopularBouquetsAsync(int count, CancellationToken ct = default)
        {
            return await _db.Products.AsNoTracking()
                .Where(p => p.IsAvailable && p.Type == ProductType.Bouquet)
                .OrderBy(p => p.Id)
                .Take(count)
                .ToListAsync(ct);
        }
    }
}
