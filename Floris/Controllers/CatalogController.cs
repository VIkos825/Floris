using Floris.Services;
using Floris.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Floris.Controllers
{
    /// <summary>Контроллер каталога товаров выбранного магазина.</summary>
    public class CatalogController : Controller
    {
        private readonly ICatalogService _catalog;
        private readonly IStoreSelectionService _selection;

        /// <summary>Создаёт контроллер.</summary>
        /// <param name="catalog">Сервис каталога.</param>
        /// <param name="selection">Сервис выбора магазина.</param>
        public CatalogController(ICatalogService catalog, IStoreSelectionService selection)
        {
            _catalog = catalog;
            _selection = selection;
        }

        /// <summary>Список товаров с фильтрами. Если магазин не выбран, предлагает его выбрать.</summary>
        /// <param name="filter">Параметры фильтра из строки запроса.</param>
        /// <param name="ct">Токен отмены запроса.</param>
        /// <returns>Страница каталога или переход к выбору магазина.</returns>
        public async Task<IActionResult> Index([FromQuery] CatalogFilter filter, CancellationToken ct)
        {
            var store = await _selection.GetSelectedStoreAsync(ct);
            if (store == null)
            {
                return RedirectToStoreSelect();
            }

            filter.Normalize();
            var model = new CatalogViewModel
            {
                Store = store,
                Filter = filter,
                Categories = await _catalog.GetCategoryTreeAsync(ct),
                Items = await _catalog.GetCatalogAsync(store.Id, filter, ct)
            };
            return View(model);
        }

        /// <summary>Страница товара.</summary>
        /// <param name="id">Идентификатор товара.</param>
        /// <param name="ct">Токен отмены запроса.</param>
        /// <returns>Страница товара, 404 или переход к выбору магазина.</returns>
        public async Task<IActionResult> Details(int id, CancellationToken ct)
        {
            var store = await _selection.GetSelectedStoreAsync(ct);
            if (store == null)
            {
                return RedirectToStoreSelect();
            }

            var item = await _catalog.GetProductAsync(store.Id, id, ct);
            if (item == null)
            {
                return NotFound();
            }

            var model = new ProductDetailsViewModel
            {
                Store = store,
                Item = item,
                Related = await _catalog.GetRelatedAsync(store.Id, item.Id, item.CategoryId, 3, ct)
            };
            return View(model);
        }

        /// <summary>Перенаправляет на выбор магазина с возвратом на текущую страницу.</summary>
        /// <returns>Результат перенаправления.</returns>
        private IActionResult RedirectToStoreSelect()
        {
            var returnUrl = $"{Request.Path}{Request.QueryString}";
            return RedirectToAction("Select", "Store", new { returnUrl });
        }
    }
}
