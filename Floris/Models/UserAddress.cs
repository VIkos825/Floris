namespace Floris.Models
{
    public class UserAddress
    {
        public int Id { get; set; }

        public int UserId { get; set; }
        public User User { get; set; } = null!;

        public string Title { get; set; } = string.Empty; // "home", "work", "parents"
        public string FullAddress { get; set; } = string.Empty;

        public decimal? Latitude { get; set; }
        public decimal? Longitude { get; set; }

        public bool IsDefault { get; set; }
    }
}