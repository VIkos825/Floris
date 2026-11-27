using Floris.Services;
using Floris.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Floris.Controllers
{
    /// <summary>Контроллер выбора города и магазина.</summary>
    public class StoreController : Controller
    {
        private readonly IStoreService _stores;
        private readonly IStoreSelectionService _selection;

        /// <summary>Создаёт контроллер.</summary>
        /// <param name="stores">Сервис магазинов.</param>
        /// <param name="selection">Сервис выбора магазина.</param>
        public StoreController(IStoreService stores, IStoreSelectionService selection)
        {
            _stores = stores;
            _selection = selection;
        }

        /// <summary>Показывает страницу выбора города и магазина.</summary>
        /// <param name="returnUrl">Локальный адрес, куда вернуться после выбора.</param>
        /// <param name="ct">Токен отмены запроса.</param>
        /// <returns>Страница выбора магазина.</returns>
        [HttpGet]
        public async Task<IActionResult> Select(string? returnUrl, CancellationToken ct)
        {
            var model = new StoreSelectViewModel
            {
                Cities = await _stores.GetCitiesWithStoresAsync(ct),
                SelectedStoreId = _selection.GetSelectedStoreId(),
                ReturnUrl = returnUrl
            };
            return View(model);
        }

        /// <summary>Сохраняет выбранный магазин и возвращает пользователя назад.</summary>
        /// <param name="storeId">Идентификатор выбранного магазина.</param>
        /// <param name="returnUrl">Локальный адрес, куда вернуться после выбора.</param>
        /// <param name="ct">Токен отмены запроса.</param>
        /// <returns>Переход на исходную страницу или в каталог.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Select(int storeId, string? returnUrl, CancellationToken ct)
        {
            var store = await _stores.GetActiveStoreAsync(storeId, ct);
            if (store == null)
            {
                TempData["Error"] = "Этот магазин недоступен. Выберите другой.";
                return RedirectToAction(nameof(Select), new { returnUrl });
            }

            _selection.SelectStore(store.Id);

            if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
            {
                return LocalRedirect(returnUrl);
            }
            return RedirectToAction("Index", "Catalog");
        }
    }
}
