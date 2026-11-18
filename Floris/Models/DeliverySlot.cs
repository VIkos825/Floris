namespace Floris.Models
{
    public class DeliverySlot
    {
        public int Id { get; set; }

        public int StoreId { get; set; }
        public Store Store { get; set; } = null!;

        public DateTime SlotDate { get; set; }
        public TimeSpan TimeFrom { get; set; }
        public TimeSpan TimeTo { get; set; }

        public int MaxOrders { get; set; } = 10;
        public int BookedOrders { get; set; } = 0;

        public decimal Surcharge { get; set; } = 0; // наценка за срочность
    }
}