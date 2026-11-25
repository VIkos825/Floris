namespace Floris.Models
{
    /// <summary>Букет, собранный клиентом в конструкторе.</summary>
    public class CustomBouquet
    {
        /// <summary>Идентификатор букета.</summary>
        public int Id { get; set; }

        /// <summary>Цветовая гамма.</summary>
        public string? Palette { get; set; }

        /// <summary>Повод.</summary>
        public string? Occasion { get; set; }

        /// <summary>Желаемый бюджет.</summary>
        public decimal TargetBudget { get; set; }

        /// <summary>Стиль.</summary>
        public string? Style { get; set; }

        /// <summary>Итоговая стоимость букета.</summary>
        public decimal TotalPrice { get; set; }

        /// <summary>Дата создания (UTC).</summary>
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        /// <summary>Состав букета.</summary>
        public ICollection<BouquetComponent> Components { get; set; } = new List<BouquetComponent>();
    }
}
