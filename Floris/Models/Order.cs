namespace Floris.Models
{
    /// <summary>Заказ клиента.</summary>
    public class Order
    {
        /// <summary>Идентификатор заказа.</summary>
        public int Id { get; set; }

        /// <summary>Идентификатор клиента.</summary>
        public int UserId { get; set; }

        /// <summary>Клиент, оформивший заказ.</summary>
        public User User { get; set; } = null!;

        /// <summary>Идентификатор магазина, который выполняет заказ.</summary>
        public int StoreId { get; set; }

        /// <summary>Магазин.</summary>
        public Store Store { get; set; } = null!;

        /// <summary>Способ получения: доставка или самовывоз.</summary>
        public DeliveryMethod DeliveryMethod { get; set; } = DeliveryMethod.Delivery;

        /// <summary>Идентификатор адреса доставки (null при самовывозе).</summary>
        public int? DeliveryAddressId { get; set; }

        /// <summary>Адрес доставки.</summary>
        public UserAddress? DeliveryAddress { get; set; }

        /// <summary>Идентификатор выбранного слота (дата и время доставки или самовывоза).</summary>
        public int DeliverySlotId { get; set; }

        /// <summary>Выбранный слот.</summary>
        public DeliverySlot DeliverySlot { get; set; } = null!;

        /// <summary>Имя получателя.</summary>
        public string RecipientName { get; set; } = string.Empty;

        /// <summary>Телефон получателя.</summary>
        public string RecipientPhone { get; set; } = string.Empty;

        /// <summary>Текущий статус заказа.</summary>
        public OrderStatus Status { get; set; } = OrderStatus.New;

        /// <summary>Стоимость доставки.</summary>
        public decimal DeliveryCost { get; set; }

        /// <summary>Итоговая сумма заказа (товары + доставка).</summary>
        public decimal TotalAmount { get; set; }

        /// <summary>Анонимный заказ: получателю не называют цену и отправителя.</summary>
        public bool IsAnonymous { get; set; }

        /// <summary>Комментарий клиента.</summary>
        public string? CustomerComment { get; set; }

        /// <summary>Дата создания (UTC).</summary>
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        /// <summary>Позиции заказа.</summary>
        public ICollection<OrderItem> Items { get; set; } = new List<OrderItem>();

        /// <summary>Фото-отчёты флориста.</summary>
        public ICollection<PhotoReport> Photos { get; set; } = new List<PhotoReport>();

        /// <summary>Платежи по заказу.</summary>
        public ICollection<Payment> Payments { get; set; } = new List<Payment>();
    }
}
