namespace Floris.Models
{
    public class Inventory
    {
        public int Id { get; set; }

        public int ProductId { get; set; }
        public Product Product { get; set; } = null!;

        public int StoreId { get; set; }
        public Store Store { get; set; } = null!;

        public int Quantity { get; set; }

        public decimal CurrentPrice { get; set; } // с учётом уценки

        public DateTime ReceivedAt { get; set; }
        public DateTime ExpiresAt { get; set; }

        public string Status { get; set; } = "available"; // "available", "low", "out_of_stock", "markdown"
    }
}