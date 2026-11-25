namespace Floris.Models
{
    /// <summary>Временной слот доставки или самовывоза в конкретном магазине.</summary>
    public class DeliverySlot
    {
        /// <summary>Идентификатор слота.</summary>
        public int Id { get; set; }

        /// <summary>Идентификатор магазина.</summary>
        public int StoreId { get; set; }

        /// <summary>Магазин.</summary>
        public Store Store { get; set; } = null!;

        /// <summary>Дата слота.</summary>
        public DateTime SlotDate { get; set; }

        /// <summary>Начало интервала.</summary>
        public TimeSpan TimeFrom { get; set; }

        /// <summary>Конец интервала.</summary>
        public TimeSpan TimeTo { get; set; }

        /// <summary>Максимум заказов в слоте.</summary>
        public int MaxOrders { get; set; } = 10;

        /// <summary>Сколько заказов уже забронировано.</summary>
        public int BookedOrders { get; set; } = 0;

        /// <summary>Наценка за срочность.</summary>
        public decimal Surcharge { get; set; } = 0;

        /// <summary>Есть ли в слоте свободные места (вычисляемое свойство, в БД не хранится).</summary>
        public bool HasFreePlaces => BookedOrders < MaxOrders;
    }
}
