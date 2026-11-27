using Floris.Models;

namespace Floris.ViewModels
{
    /// <summary>Данные для главной страницы.</summary>
    public class HomeViewModel
    {
        /// <summary>Выбранный магазин (null, если не выбран).</summary>
        public Store? SelectedStore { get; set; }

        /// <summary>Популярные готовые букеты.</summary>
        public IReadOnlyList<CatalogItemViewModel> PopularBouquets { get; set; } = new List<CatalogItemViewModel>();
    }
}
