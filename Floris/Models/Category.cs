namespace Floris.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        public int? ParentId { get; set; } // self-reference
        public Category? Parent { get; set; }

        public string Slug { get; set; } = string.Empty;

        public ICollection<Category> Children { get; set; } = new List<Category>();
        public ICollection<Product> Products { get; set; } = new List<Product>();
    }
}