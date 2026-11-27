using Floris.Models;

namespace Floris.ViewModels
{
    /// <summary>Данные страницы каталога.</summary>
    public class CatalogViewModel
    {
        /// <summary>Выбранный магазин.</summary>
        public Store Store { get; set; } = null!;

        /// <summary>Текущие параметры фильтра.</summary>
        public CatalogFilter Filter { get; set; } = new();

        /// <summary>Дерево категорий.</summary>
        public IReadOnlyList<CategoryNode> Categories { get; set; } = new List<CategoryNode>();

        /// <summary>Товары, подошедшие под фильтр.</summary>
        public IReadOnlyList<CatalogItemViewModel> Items { get; set; } = new List<CatalogItemViewModel>();

        /// <summary>Собирает параметры ссылки с сохранением текущих фильтров и новой категорией.</summary>
        /// <param name="category">Короткое имя категории (null — без категории).</param>
        /// <returns>Словарь параметров для tag helper'а ссылки.</returns>
        public Dictionary<string, string> RouteValues(string? category)
        {
            var values = new Dictionary<string, string>();
            if (!string.IsNullOrEmpty(category)) values["category"] = category;
            if (!string.IsNullOrEmpty(Filter.Search)) values["search"] = Filter.Search;
            if (!string.IsNullOrEmpty(Filter.Sort) && Filter.Sort != CatalogSort.Popular) values["sort"] = Filter.Sort;
            if (Filter.InStockOnly) values["inStockOnly"] = "true";
            return values;
        }
    }
}
