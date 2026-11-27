using Floris.Models;

namespace Floris.Services
{
    /// <summary>Сервис чтения городов и магазинов.</summary>
    public interface IStoreService
    {
        /// <summary>Возвращает города, в которых есть хотя бы один работающий магазин, вместе с магазинами.</summary>
        /// <param name="ct">Токен отмены запроса.</param>
        /// <returns>Список городов с магазинами.</returns>
        Task<IReadOnlyList<City>> GetCitiesWithStoresAsync(CancellationToken ct = default);

        /// <summary>Возвращает работающий магазин вместе с городом.</summary>
        /// <param name="storeId">Идентификатор магазина.</param>
        /// <param name="ct">Токен отмены запроса.</param>
        /// <returns>Магазин или null, если его нет либо он закрыт.</returns>
        Task<Store?> GetActiveStoreAsync(int storeId, CancellationToken ct = default);
    }
}
