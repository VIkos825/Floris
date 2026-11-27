using System.Diagnostics;
using Floris.Services;
using Floris.Settings;
using Floris.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Floris.Controllers
{
    /// <summary>Контроллер общих страниц: главная, «О проекте», контакты, ошибки.</summary>
    public class HomeController : Controller
    {
        private readonly ICatalogService _catalog;
        private readonly IStoreSelectionService _selection;
        private readonly ProjectInfoSettings _projectInfo;

        /// <summary>Создаёт контроллер.</summary>
        /// <param name="catalog">Сервис каталога.</param>
        /// <param name="selection">Сервис выбора магазина.</param>
        /// <param name="projectInfo">Сведения о курсовом проекте из конфигурации.</param>
        public HomeController(ICatalogService catalog, IStoreSelectionService selection,
                              IOptions<ProjectInfoSettings> projectInfo)
        {
            _catalog = catalog;
            _selection = selection;
            _projectInfo = projectInfo.Value;
        }

        /// <summary>Главная страница.</summary>
        /// <param name="ct">Токен отмены запроса.</param>
        /// <returns>Представление главной страницы.</returns>
        public async Task<IActionResult> Index(CancellationToken ct)
        {
            var store = await _selection.GetSelectedStoreAsync(ct);
            var model = new HomeViewModel
            {
                SelectedStore = store,
                PopularBouquets = await _catalog.GetPopularBouquetsAsync(store?.Id, 6, ct)
            };
            return View(model);
        }

        /// <summary>Окно с информацией о курсовом проекте.</summary>
        /// <returns>Представление «О проекте».</returns>
        public IActionResult About()
        {
            return View(_projectInfo);
        }

        /// <summary>Страница контактов.</summary>
        /// <returns>Представление «Контакты».</returns>
        public IActionResult Contacts()
        {
            return View();
        }

        /// <summary>Страница ошибки (исключения и коды 404 и т. п.).</summary>
        /// <param name="code">HTTP-код, если страница вызвана из UseStatusCodePages.</param>
        /// <returns>Представление ошибки.</returns>
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error(int? code)
        {
            var status = code ?? 500;
            var model = new ErrorViewModel
            {
                StatusCode = status,
                Message = status == 404
                    ? "Такой страницы не существует."
                    : "Что-то пошло не так. Попробуйте повторить действие позже.",
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            };
            Response.StatusCode = status;
            return View(model);
        }
    }
}
