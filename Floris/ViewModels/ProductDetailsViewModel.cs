using Floris.Models;

namespace Floris.ViewModels
{
    /// <summary>Данные страницы товара.</summary>
    public class ProductDetailsViewModel
    {
        /// <summary>Выбранный магазин.</summary>
        public Store Store { get; set; } = null!;

        /// <summary>Товар.</summary>
        public CatalogItemViewModel Item { get; set; } = null!;

        /// <summary>Похожие товары из той же категории.</summary>
        public IReadOnlyList<CatalogItemViewModel> Related { get; set; } = new List<CatalogItemViewModel>();
    }
}
