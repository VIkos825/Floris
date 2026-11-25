using System.ComponentModel.DataAnnotations;

namespace Floris.Models
{
    /// <summary>Магазин сети. Каталог и остатки привязаны к магазину.</summary>
    public class Store
    {
        /// <summary>Идентификатор магазина.</summary>
        public int Id { get; set; }

        /// <summary>Идентификатор города.</summary>
        public int CityId { get; set; }

        /// <summary>Город магазина.</summary>
        public City City { get; set; } = null!;

        /// <summary>Название магазина.</summary>
        [Required, MaxLength(200)]
        public string Name { get; set; } = string.Empty;

        /// <summary>Адрес магазина.</summary>
        [MaxLength(300)]
        public string Address { get; set; } = string.Empty;

        /// <summary>Широта (для карты).</summary>
        public decimal Latitude { get; set; }

        /// <summary>Долгота (для карты).</summary>
        public decimal Longitude { get; set; }

        /// <summary>Телефон магазина.</summary>
        [MaxLength(20)]
        public string? Phone { get; set; }

        /// <summary>Признак работающего магазина.</summary>
        public bool IsActive { get; set; } = true;

        /// <summary>Фиксированная стоимость доставки.</summary>
        public decimal DeliveryFlatRate { get; set; } = 300;

        /// <summary>Сумма заказа, начиная с которой доставка бесплатна.</summary>
        public decimal FreeDeliveryAbove { get; set; } = 3000;

        /// <summary>Партии товаров на складе магазина.</summary>
        public ICollection<Inventory> Inventory { get; set; } = new List<Inventory>();

        /// <summary>Слоты доставки и самовывоза.</summary>
        public ICollection<DeliverySlot> DeliverySlots { get; set; } = new List<DeliverySlot>();
    }
}
