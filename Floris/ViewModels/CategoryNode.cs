namespace Floris.ViewModels
{
    /// <summary>Узел дерева категорий каталога (категория и её подкатегории).</summary>
    public class CategoryNode
    {
        /// <summary>Идентификатор категории.</summary>
        public int Id { get; set; }

        /// <summary>Название категории.</summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>Короткое имя для адреса страницы.</summary>
        public string Slug { get; set; } = string.Empty;

        /// <summary>Дочерние категории.</summary>
        public List<CategoryNode> Children { get; } = new();

        /// <summary>Возвращает идентификаторы этой категории и всех вложенных (рекурсивно).</summary>
        /// <returns>Последовательность идентификаторов.</returns>
        public IEnumerable<int> SelfAndDescendantIds()
        {
            yield return Id;
            foreach (var child in Children)
            {
                foreach (var id in child.SelfAndDescendantIds())
                {
                    yield return id;
                }
            }
        }

        /// <summary>Ищет категорию по короткому имени среди узлов и их потомков.</summary>
        /// <param name="nodes">Узлы, с которых начинается поиск.</param>
        /// <param name="slug">Искомое короткое имя.</param>
        /// <returns>Найденный узел или null.</returns>
        public static CategoryNode? FindBySlug(IEnumerable<CategoryNode> nodes, string slug)
        {
            foreach (var node in nodes)
            {
                if (string.Equals(node.Slug, slug, StringComparison.OrdinalIgnoreCase))
                {
                    return node;
                }

                var inChildren = FindBySlug(node.Children, slug);
                if (inChildren != null)
                {
                    return inChildren;
                }
            }
            return null;
        }
    }
}
