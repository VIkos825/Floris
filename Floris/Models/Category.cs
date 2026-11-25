using System.ComponentModel.DataAnnotations;

namespace Floris.Models
{
    /// <summary>Категория каталога. Образует дерево через <see cref="ParentId"/>.</summary>
    public class Category
    {
        /// <summary>Идентификатор категории.</summary>
        public int Id { get; set; }

        /// <summary>Название категории.</summary>
        [Required, MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        /// <summary>Идентификатор родительской категории (null для корневой).</summary>
        public int? ParentId { get; set; }

        /// <summary>Родительская категория.</summary>
        public Category? Parent { get; set; }

        /// <summary>Короткое имя для адреса страницы (например, krasnye-rozy). Уникально.</summary>
        [Required, MaxLength(100)]
        public string Slug { get; set; } = string.Empty;

        /// <summary>Дочерние категории.</summary>
        public ICollection<Category> Children { get; set; } = new List<Category>();

        /// <summary>Товары категории.</summary>
        public ICollection<Product> Products { get; set; } = new List<Product>();
    }
}
