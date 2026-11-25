using Floris.Models;

namespace Floris.ViewModels
{
    /// <summary>Данные для главной страницы.</summary>
    public class HomeViewModel
    {
        /// <summary>Города для выбора.</summary>
        public IReadOnlyList<City> Cities { get; set; } = new List<City>();

        /// <summary>Популярные готовые букеты.</summary>
        public IReadOnlyList<Product> PopularBouquets { get; set; } = new List<Product>();
    }
}
