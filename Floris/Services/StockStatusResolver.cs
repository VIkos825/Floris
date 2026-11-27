using Floris.Models;

namespace Floris.Services
{
    /// <summary>Определяет итоговый статус наличия товара в магазине по сумме остатков.</summary>
    public static class StockStatusResolver
    {
        /// <summary>Остаток, при котором (и ниже) товар считается «Мало».</summary>
        public const int LowStockThreshold = 5;

        /// <summary>Вычисляет статус наличия.</summary>
        /// <param name="totalQuantity">Суммарный остаток по всем партиям магазина.</param>
        /// <param name="hasMarkdownBatch">Есть ли среди партий уценённая.</param>
        /// <returns>Статус: нет в наличии, уценка, мало или в наличии.</returns>
        public static StockStatus Resolve(int totalQuantity, bool hasMarkdownBatch)
        {
            if (totalQuantity <= 0) return StockStatus.OutOfStock;
            if (hasMarkdownBatch) return StockStatus.Markdown;
            if (totalQuantity <= LowStockThreshold) return StockStatus.Low;
            return StockStatus.Available;
        }
    }
}
