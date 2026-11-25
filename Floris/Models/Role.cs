namespace Floris.Models
{
    /// <summary>Имена ролей пользователей системы (используются в атрибутах авторизации).</summary>
    public static class RoleNames
    {
        /// <summary>Клиент магазина.</summary>
        public const string Client = "client";
        /// <summary>Флорист.</summary>
        public const string Florist = "florist";
        /// <summary>Курьер.</summary>
        public const string Courier = "courier";
        /// <summary>Администратор.</summary>
        public const string Admin = "admin";
    }

    /// <summary>Роль пользователя. У каждого пользователя ровно одна роль.</summary>
    public class Role
    {
        /// <summary>Идентификатор роли.</summary>
        public int Id { get; set; }

        /// <summary>Имя роли (см. <see cref="RoleNames"/>).</summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>Пользователи с этой ролью.</summary>
        public ICollection<User> Users { get; set; } = new List<User>();
    }
}
