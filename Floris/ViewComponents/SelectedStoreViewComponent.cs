using Floris.Services;
using Floris.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Floris.ViewComponents
{
    /// <summary>Блок в шапке сайта: показывает выбранный магазин или ссылку на выбор.</summary>
    public class SelectedStoreViewComponent : ViewComponent
    {
        private readonly IStoreSelectionService _selection;

        /// <summary>Создаёт компонент.</summary>
        /// <param name="selection">Сервис выбора магазина.</param>
        public SelectedStoreViewComponent(IStoreSelectionService selection)
        {
            _selection = selection;
        }

        /// <summary>Готовит данные для отображения.</summary>
        /// <returns>Представление блока.</returns>
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var store = await _selection.GetSelectedStoreAsync(HttpContext.RequestAborted);
            return View(new SelectedStoreViewModel { Store = store });
        }
    }
}
