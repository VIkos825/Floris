using Floris.ViewModels;

namespace Floris.Services
{
    /// <summary>Сервис чтения каталога с учётом наличия и цен выбранного магазина.</summary>
    public interface ICatalogService
    {
        /// <summary>Возвращает дерево категорий (корневые категории с подкатегориями).</summary>
        /// <param name="ct">Токен отмены запроса.</param>
        /// <returns>Корневые узлы дерева.</returns>
        Task<IReadOnlyList<CategoryNode>> GetCategoryTreeAsync(CancellationToken ct = default);

        /// <summary>Возвращает товары каталога магазина с учётом фильтра и сортировки.</summary>
        /// <param name="storeId">Идентификатор магазина.</param>
        /// <param name="filter">Параметры фильтра (уже нормализованные).</param>
        /// <param name="ct">Токен отмены запроса.</param>
        /// <returns>Список товаров.</returns>
        Task<IReadOnlyList<CatalogItemViewModel>> GetCatalogAsync(int storeId, CatalogFilter filter, CancellationToken ct = default);

        /// <summary>Возвращает один товар с ценой и наличием в магазине.</summary>
        /// <param name="storeId">Идентификатор магазина.</param>
        /// <param name="productId">Идентификатор товара.</param>
        /// <param name="ct">Токен отмены запроса.</param>
        /// <returns>Товар или null, если его нет либо он снят с продажи.</returns>
        Task<CatalogItemViewModel?> GetProductAsync(int storeId, int productId, CancellationToken ct = default);

        /// <summary>Возвращает товары из той же категории (кроме текущего), которые есть в наличии.</summary>
        /// <param name="storeId">Идентификатор магазина.</param>
        /// <param name="productId">Идентификатор текущего товара (исключается).</param>
        /// <param name="categoryId">Идентификатор категории.</param>
        /// <param name="count">Сколько товаров вернуть.</param>
        /// <param name="ct">Токен отмены запроса.</param>
        /// <returns>Список похожих товаров.</returns>
        Task<IReadOnlyList<CatalogItemViewModel>> GetRelatedAsync(int storeId, int productId, int categoryId, int count, CancellationToken ct = default);

        /// <summary>Возвращает готовые букеты для главной страницы.</summary>
        /// <param name="storeId">Идентификатор магазина или null, если магазин не выбран (тогда показывается базовая цена).</param>
        /// <param name="count">Сколько букетов вернуть.</param>
        /// <param name="ct">Токен отмены запроса.</param>
        /// <returns>Список букетов.</returns>
        Task<IReadOnlyList<CatalogItemViewModel>> GetPopularBouquetsAsync(int? storeId, int count, CancellationToken ct = default);
    }
}
