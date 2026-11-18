using System.ComponentModel.DataAnnotations;

namespace Floris.Models
{
    public class Order
    {
        public int Id { get; set; }

        public int UserId { get; set; }
        public User User { get; set; } = null!;

        public int StoreId { get; set; }
        public Store Store { get; set; } = null!;

        public int DeliveryAddressId { get; set; }
        public UserAddress DeliveryAddress { get; set; } = null!;

        public int DeliverySlotId { get; set; }
        public DeliverySlot DeliverySlot { get; set; } = null!;

        public string RecipientName { get; set; } = string.Empty;
        public string RecipientPhone { get; set; } = string.Empty;

        public DateTime DeliveryDate { get; set; }

        public string Status { get; set; } = "new"; // "new", "confirmed", "assembling", "ready", "delivering", "delivered", "cancelled"

        public decimal TotalAmount { get; set; }

        public bool IsAnonymous { get; set; }

        public string? CustomerComment { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public ICollection<OrderItem> Items { get; set; } = new List<OrderItem>();
        public ICollection<PhotoReport> Photos { get; set; } = new List<PhotoReport>();
        public ICollection<Payment> Payments { get; set; } = new List<Payment>();
    }
}