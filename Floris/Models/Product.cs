using System.ComponentModel.DataAnnotations;

namespace Floris.Models
{
    /// <summary>Товар каталога: цветок, готовый букет или сопутствующий товар.</summary>
    public class Product
    {
        /// <summary>Идентификатор товара.</summary>
        public int Id { get; set; }

        /// <summary>Идентификатор категории.</summary>
        public int CategoryId { get; set; }

        /// <summary>Категория товара.</summary>
        public Category Category { get; set; } = null!;

        /// <summary>Название.</summary>
        [Required, MaxLength(200)]
        public string Name { get; set; } = string.Empty;

        /// <summary>Описание и состав.</summary>
        [MaxLength(2000)]
        public string? Description { get; set; }

        /// <summary>Тип товара.</summary>
        public ProductType Type { get; set; } = ProductType.Flower;

        /// <summary>Базовая цена.</summary>
        public decimal BasePrice { get; set; }

        /// <summary>Адрес фотографии (null — используется заглушка).</summary>
        [MaxLength(500)]
        public string? PhotoUrl { get; set; }

        /// <summary>Доступен ли товар для продажи.</summary>
        public bool IsAvailable { get; set; } = true;

        /// <summary>Срок хранения в днях (для расчёта уценки).</summary>
        public int ShelfLifeDays { get; set; } = 3;

        /// <summary>Партии товара по магазинам.</summary>
        public ICollection<Inventory> Inventory { get; set; } = new List<Inventory>();
    }
}
