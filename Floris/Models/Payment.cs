namespace Floris.Models
{
    /// <summary>Платёж по заказу.</summary>
    public class Payment
    {
        /// <summary>Идентификатор платежа.</summary>
        public int Id { get; set; }

        /// <summary>Идентификатор заказа.</summary>
        public int OrderId { get; set; }

        /// <summary>Заказ.</summary>
        public Order Order { get; set; } = null!;

        /// <summary>Способ оплаты.</summary>
        public PaymentMethod Method { get; set; } = PaymentMethod.Card;

        /// <summary>Сумма платежа.</summary>
        public decimal Amount { get; set; }

        /// <summary>Статус платежа.</summary>
        public PaymentStatus Status { get; set; } = PaymentStatus.Pending;

        /// <summary>Идентификатор транзакции во внешней системе.</summary>
        public string? TransactionId { get; set; }

        /// <summary>Дата оплаты (UTC).</summary>
        public DateTime? PaidAt { get; set; }
    }
}
