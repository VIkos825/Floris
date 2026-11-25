namespace Floris.Models
{
    /// <summary>Сохранённый адрес доставки пользователя.</summary>
    public class UserAddress
    {
        /// <summary>Идентификатор адреса.</summary>
        public int Id { get; set; }

        /// <summary>Идентификатор владельца.</summary>
        public int UserId { get; set; }

        /// <summary>Владелец адреса.</summary>
        public User User { get; set; } = null!;

        /// <summary>Название адреса («Дом», «Работа», «Родители»).</summary>
        public string Title { get; set; } = string.Empty;

        /// <summary>Полный адрес.</summary>
        public string FullAddress { get; set; } = string.Empty;

        /// <summary>Широта (необязательно).</summary>
        public decimal? Latitude { get; set; }

        /// <summary>Долгота (необязательно).</summary>
        public decimal? Longitude { get; set; }

        /// <summary>Признак адреса по умолчанию.</summary>
        public bool IsDefault { get; set; }
    }
}
