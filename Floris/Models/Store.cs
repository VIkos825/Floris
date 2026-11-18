using System.ComponentModel.DataAnnotations;

namespace Floris.Models
{
    public class Store
    {
        public int Id { get; set; }

        public int CityId { get; set; }
        public City City { get; set; } = null!;

        [Required]
        public string Name { get; set; } = string.Empty;

        public string Address { get; set; } = string.Empty;

        public decimal Latitude { get; set; }  // для карты
        public decimal Longitude { get; set; } // для карты

        public string? Phone { get; set; }

        public bool IsActive { get; set; } = true;

        public decimal DeliveryFlatRate { get; set; } = 300; // фикс. стоимость доставки
        public decimal FreeDeliveryAbove { get; set; } = 3000; // бесплатно от этой суммы

        public ICollection<Inventory> Inventory { get; set; } = new List<Inventory>();
        public ICollection<DeliverySlot> DeliverySlots { get; set; } = new List<DeliverySlot>();
    }
}