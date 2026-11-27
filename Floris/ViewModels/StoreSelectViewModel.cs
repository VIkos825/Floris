using Floris.Models;

namespace Floris.ViewModels
{
    /// <summary>Данные страницы выбора города и магазина.</summary>
    public class StoreSelectViewModel
    {
        /// <summary>Города с активными магазинами.</summary>
        public IReadOnlyList<City> Cities { get; set; } = new List<City>();

        /// <summary>Идентификатор уже выбранного магазина (если есть).</summary>
        public int? SelectedStoreId { get; set; }

        /// <summary>Адрес, на который вернуть пользователя после выбора.</summary>
        public string? ReturnUrl { get; set; }
    }
}
