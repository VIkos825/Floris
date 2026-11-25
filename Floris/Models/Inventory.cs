namespace Floris.Models
{
    /// <summary>Партия товара на складе магазина (количество, цена с учётом уценки, срок годности).</summary>
    public class Inventory
    {
        /// <summary>Идентификатор записи.</summary>
        public int Id { get; set; }

        /// <summary>Идентификатор товара.</summary>
        public int ProductId { get; set; }

        /// <summary>Товар.</summary>
        public Product Product { get; set; } = null!;

        /// <summary>Идентификатор магазина.</summary>
        public int StoreId { get; set; }

        /// <summary>Магазин.</summary>
        public Store Store { get; set; } = null!;

        /// <summary>Количество в партии.</summary>
        public int Quantity { get; set; }

        /// <summary>Текущая цена с учётом уценки.</summary>
        public decimal CurrentPrice { get; set; }

        /// <summary>Дата поступления партии (UTC).</summary>
        public DateTime ReceivedAt { get; set; }

        /// <summary>Дата, до которой товар сохраняет товарный вид (UTC).</summary>
        public DateTime ExpiresAt { get; set; }

        /// <summary>Статус партии.</summary>
        public StockStatus Status { get; set; } = StockStatus.Available;
    }
}
