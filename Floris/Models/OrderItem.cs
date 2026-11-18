namespace Floris.Models
{
    public class OrderItem
    {
        public int Id { get; set; }

        public int OrderId { get; set; }
        public Order Order { get; set; } = null!;

        public int? ProductId { get; set; } // XOR с CustomBouquetId
        public Product? Product { get; set; }

        public int? CustomBouquetId { get; set; } // XOR с ProductId
        public CustomBouquet? CustomBouquet { get; set; }

        public int Quantity { get; set; }

        public decimal UnitPrice { get; set; }
    }
}