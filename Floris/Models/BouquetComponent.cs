namespace Floris.Models
{
    /// <summary>Компонент собранного букета (цветок, зелень, упаковка).</summary>
    public class BouquetComponent
    {
        /// <summary>Идентификатор компонента.</summary>
        public int Id { get; set; }

        /// <summary>Идентификатор букета.</summary>
        public int CustomBouquetId { get; set; }

        /// <summary>Букет.</summary>
        public CustomBouquet CustomBouquet { get; set; } = null!;

        /// <summary>Идентификатор товара.</summary>
        public int ProductId { get; set; }

        /// <summary>Товар.</summary>
        public Product Product { get; set; } = null!;

        /// <summary>Роль компонента в букете.</summary>
        public BouquetComponentRole Role { get; set; } = BouquetComponentRole.Base;

        /// <summary>Количество.</summary>
        public int Quantity { get; set; }
    }
}
