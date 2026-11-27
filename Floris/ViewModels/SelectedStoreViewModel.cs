using Floris.Models;

namespace Floris.ViewModels
{
    /// <summary>Данные блока «Ваш магазин» в шапке сайта.</summary>
    public class SelectedStoreViewModel
    {
        /// <summary>Выбранный магазин или null, если магазин ещё не выбран.</summary>
        public Store? Store { get; set; }
    }
}
