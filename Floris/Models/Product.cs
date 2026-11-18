using System.ComponentModel.DataAnnotations;

namespace Floris.Models
{
    public class Product
    {
        public int Id { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; } = null!;

        [Required]
        public string Name { get; set; } = string.Empty;

        public string? Description { get; set; }

        public string Type { get; set; } = "flower"; // "flower", "bouquet", "accessory"

        public decimal BasePrice { get; set; }

        public string? PhotoUrl { get; set; }

        public bool IsAvailable { get; set; } = true;

        public int ShelfLifeDays { get; set; } = 3;

        public ICollection<Inventory> Inventory { get; set; } = new List<Inventory>();
    }
}