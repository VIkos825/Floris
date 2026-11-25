namespace Floris.Models
{
    /// <summary>Позиция заказа: либо товар каталога, либо собранный букет (одно из двух).</summary>
    public class OrderItem
    {
        /// <summary>Идентификатор позиции.</summary>
        public int Id { get; set; }

        /// <summary>Идентификатор заказа.</summary>
        public int OrderId { get; set; }

        /// <summary>Заказ.</summary>
        public Order Order { get; set; } = null!;

        /// <summary>Идентификатор товара (заполнено либо оно, либо <see cref="CustomBouquetId"/>).</summary>
        public int? ProductId { get; set; }

        /// <summary>Товар каталога.</summary>
        public Product? Product { get; set; }

        /// <summary>Идентификатор собранного букета.</summary>
        public int? CustomBouquetId { get; set; }

        /// <summary>Собранный букет.</summary>
        public CustomBouquet? CustomBouquet { get; set; }

        /// <summary>Количество.</summary>
        public int Quantity { get; set; }

        /// <summary>Цена за единицу на момент заказа.</summary>
        public decimal UnitPrice { get; set; }
    }
}
