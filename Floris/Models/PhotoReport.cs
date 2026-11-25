namespace Floris.Models
{
    /// <summary>Фотография собранного заказа, которую загружает флорист.</summary>
    public class PhotoReport
    {
        /// <summary>Идентификатор фото.</summary>
        public int Id { get; set; }

        /// <summary>Идентификатор заказа.</summary>
        public int OrderId { get; set; }

        /// <summary>Заказ.</summary>
        public Order Order { get; set; } = null!;

        /// <summary>Адрес файла изображения.</summary>
        public string Url { get; set; } = string.Empty;

        /// <summary>Тип фото.</summary>
        public PhotoType Type { get; set; } = PhotoType.Final;

        /// <summary>Идентификатор сотрудника, загрузившего фото.</summary>
        public int UploadedByUserId { get; set; }

        /// <summary>Сотрудник, загрузивший фото.</summary>
        public User UploadedByUser { get; set; } = null!;

        /// <summary>Дата загрузки (UTC).</summary>
        public DateTime UploadedAt { get; set; } = DateTime.UtcNow;
    }
}
