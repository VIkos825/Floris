namespace Floris.ViewModels
{
    /// <summary>Допустимые значения сортировки каталога.</summary>
    public static class CatalogSort
    {
        /// <summary>По умолчанию.</summary>
        public const string Popular = "popular";
        /// <summary>Сначала дешёвые.</summary>
        public const string PriceAsc = "price_asc";
        /// <summary>Сначала дорогие.</summary>
        public const string PriceDesc = "price_desc";
        /// <summary>По названию.</summary>
        public const string Name = "name";

        /// <summary>Проверяет, что значение входит в список допустимых.</summary>
        /// <param name="value">Проверяемое значение.</param>
        /// <returns>true, если значение допустимо.</returns>
        public static bool IsValid(string? value) =>
            value is Popular or PriceAsc or PriceDesc or Name;
    }

    /// <summary>Параметры фильтрации каталога (берутся из строки запроса).</summary>
    public class CatalogFilter
    {
        /// <summary>Максимальная длина поискового запроса.</summary>
        public const int MaxSearchLength = 50;

        /// <summary>Короткое имя выбранной категории.</summary>
        public string? Category { get; set; }

        /// <summary>Строка поиска по названию и описанию.</summary>
        public string? Search { get; set; }

        /// <summary>Вариант сортировки (см. <see cref="CatalogSort"/>).</summary>
        public string? Sort { get; set; }

        /// <summary>Показывать только товары в наличии.</summary>
        public bool InStockOnly { get; set; }

        /// <summary>Приводит параметры к безопасному виду: обрезает пробелы и длину, сбрасывает недопустимые значения.</summary>
        public void Normalize()
        {
            Category = string.IsNullOrWhiteSpace(Category) ? null : Category.Trim();

            Search = string.IsNullOrWhiteSpace(Search) ? null : Search.Trim();
            if (Search != null && Search.Length > MaxSearchLength)
            {
                Search = Search.Substring(0, MaxSearchLength);
            }

            if (!CatalogSort.IsValid(Sort))
            {
                Sort = CatalogSort.Popular;
            }
        }
    }
}
