using Floris.Models;

namespace Floris.ViewModels
{
    /// <summary>Товар в каталоге с ценой и наличием в выбранном магазине.</summary>
    public class CatalogItemViewModel
    {
        /// <summary>Идентификатор товара.</summary>
        public int Id { get; set; }

        /// <summary>Название.</summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>Описание.</summary>
        public string? Description { get; set; }

        /// <summary>Адрес фотографии (null — заглушка).</summary>
        public string? PhotoUrl { get; set; }

        /// <summary>Идентификатор категории.</summary>
        public int CategoryId { get; set; }

        /// <summary>Название категории.</summary>
        public string CategoryName { get; set; } = string.Empty;

        /// <summary>Короткое имя категории.</summary>
        public string CategorySlug { get; set; } = string.Empty;

        /// <summary>Тип товара.</summary>
        public ProductType Type { get; set; }

        /// <summary>Актуальная цена в магазине.</summary>
        public decimal Price { get; set; }

        /// <summary>Цена до уценки (null, если уценки нет).</summary>
        public decimal? OldPrice { get; set; }

        /// <summary>Остаток в магазине.</summary>
        public int Quantity { get; set; }

        /// <summary>Статус наличия.</summary>
        public StockStatus Status { get; set; } = StockStatus.Available;

        /// <summary>Известны ли данные магазина (false — показана базовая цена без наличия).</summary>
        public bool HasStoreData { get; set; } = true;

        /// <summary>Есть ли товар в наличии.</summary>
        public bool IsInStock => Status != StockStatus.OutOfStock;

        /// <summary>Есть ли скидка.</summary>
        public bool HasDiscount => OldPrice.HasValue;
    }
}
