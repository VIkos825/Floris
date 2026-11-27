using Floris.Models;

namespace Floris.Services
{
    /// <summary>Хранит выбор магазина пользователем (в cookie) и возвращает выбранный магазин.</summary>
    public interface IStoreSelectionService
    {
        /// <summary>Читает идентификатор выбранного магазина из cookie (без проверки в базе).</summary>
        /// <returns>Идентификатор или null, если магазин не выбран.</returns>
        int? GetSelectedStoreId();

        /// <summary>Возвращает выбранный магазин. Если сохранённый магазин не существует или закрыт, сбрасывает выбор.</summary>
        /// <param name="ct">Токен отмены запроса.</param>
        /// <returns>Магазин или null.</returns>
        Task<Store?> GetSelectedStoreAsync(CancellationToken ct = default);

        /// <summary>Запоминает выбранный магазин в cookie на 30 дней.</summary>
        /// <param name="storeId">Идентификатор магазина.</param>
        void SelectStore(int storeId);

        /// <summary>Удаляет сохранённый выбор магазина.</summary>
        void ClearSelection();
    }
}
