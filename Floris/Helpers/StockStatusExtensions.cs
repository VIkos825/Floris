using Floris.Models;

namespace Floris.Helpers
{
    /// <summary>Расширения для отображения статуса наличия товара.</summary>
    public static class StockStatusExtensions
    {
        /// <summary>Возвращает текст статуса для пользователя.</summary>
        /// <param name="status">Статус наличия.</param>
        /// <returns>Русское название статуса.</returns>
        public static string ToDisplayName(this StockStatus status)
        {
            return status switch
            {
                StockStatus.Available => "В наличии",
                StockStatus.Low => "Мало",
                StockStatus.OutOfStock => "Нет в наличии",
                StockStatus.Markdown => "Уценка",
                _ => string.Empty
            };
        }

        /// <summary>Возвращает CSS-класс значка статуса.</summary>
        /// <param name="status">Статус наличия.</param>
        /// <returns>Имя CSS-класса.</returns>
        public static string ToCssClass(this StockStatus status)
        {
            return status switch
            {
                StockStatus.Available => "badge-available",
                StockStatus.Low => "badge-low",
                StockStatus.OutOfStock => "badge-out",
                StockStatus.Markdown => "badge-markdown",
                _ => string.Empty
            };
        }
    }
}
