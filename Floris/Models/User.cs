using System.ComponentModel.DataAnnotations;

namespace Floris.Models
{
    /// <summary>Пользователь системы: клиент, флорист, курьер или администратор.</summary>
    public class User
    {
        /// <summary>Идентификатор пользователя.</summary>
        public int Id { get; set; }

        /// <summary>Адрес электронной почты (логин). Уникален.</summary>
        [Required, EmailAddress, MaxLength(256)]
        public string Email { get; set; } = string.Empty;

        /// <summary>Хеш пароля. Пароль в открытом виде не хранится.</summary>
        [Required]
        public string PasswordHash { get; set; } = string.Empty;

        /// <summary>Телефон.</summary>
        [MaxLength(20)]
        public string? Phone { get; set; }

        /// <summary>Имя.</summary>
        [MaxLength(100)]
        public string? FirstName { get; set; }

        /// <summary>Фамилия.</summary>
        [MaxLength(100)]
        public string? LastName { get; set; }

        /// <summary>Дата регистрации (UTC).</summary>
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        /// <summary>Идентификатор роли.</summary>
        public int RoleId { get; set; }

        /// <summary>Роль пользователя.</summary>
        public Role Role { get; set; } = null!;

        /// <summary>Сохранённые адреса доставки.</summary>
        public ICollection<UserAddress> Addresses { get; set; } = new List<UserAddress>();

        /// <summary>Заказы пользователя.</summary>
        public ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}
