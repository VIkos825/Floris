namespace Floris.Models
{
    public class Payment
    {
        public int Id { get; set; }

        public int OrderId { get; set; }
        public Order Order { get; set; } = null!;

        public string Method { get; set; } = "card"; // "card", "sbp", "cash"
        public decimal Amount { get; set; }
        public string Status { get; set; } = "pending"; // "pending", "success", "failed", "refunded"

        public string? TransactionId { get; set; }
        public DateTime? PaidAt { get; set; }
    }
}