namespace Floris.Models
{
    public class CustomBouquet
    {
        public int Id { get; set; }

        public string? Palette { get; set; } // цветовая гамма
        public string? Occasion { get; set; } // повод
        public decimal TargetBudget { get; set; }
        public string? Style { get; set; }

        public ICollection<BouquetComponent> Components { get; set; } = new List<BouquetComponent>();
    }
}