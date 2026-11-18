namespace Floris.Models
{
    public class BouquetComponent
    {
        public int Id { get; set; }

        public int CustomBouquetId { get; set; }
        public CustomBouquet CustomBouquet { get; set; } = null!;

        public int ProductId { get; set; }
        public Product Product { get; set; } = null!;

        public string Role { get; set; } = "base"; // "base", "filler", "greenery", "packaging"

        public int Quantity { get; set; }
    }
}