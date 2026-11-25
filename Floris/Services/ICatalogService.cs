using Floris.Models;

namespace Floris.Services
{
    /// <summary>Сервис чтения каталога: города, товары.</summary>
    public interface ICatalogService
    {
        /// <summary>Возвращает все города, отсортированные по названию.</summary>
        /// <param name="ct">Токен отмены запроса.</param>
        /// <returns>Список городов.</returns>
        Task<IReadOnlyList<City>> GetCitiesAsync(CancellationToken ct = default);

        /// <summary>Возвращает доступные готовые букеты для главной страницы.</summary>
        /// <param name="count">Сколько букетов вернуть.</param>
        /// <param name="ct">Токен отмены запроса.</param>
        /// <returns>Список букетов.</returns>
        Task<IReadOnlyList<Product>> GetPopularBouquetsAsync(int count, CancellationToken ct = default);
    }
}
