using System.ComponentModel.DataAnnotations;

namespace Floris.Models
{
    /// <summary>Город, в котором работают магазины сети.</summary>
    public class City
    {
        /// <summary>Идентификатор города.</summary>
        public int Id { get; set; }

        /// <summary>Название города.</summary>
        [Required, MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        /// <summary>Часовой пояс (идентификатор IANA).</summary>
        [MaxLength(64)]
        public string Timezone { get; set; } = "Europe/Moscow";

        /// <summary>Магазины города.</summary>
        public ICollection<Store> Stores { get; set; } = new List<Store>();
    }
}
