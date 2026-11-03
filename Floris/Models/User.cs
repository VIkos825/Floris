using System.ComponentModel.DataAnnotations;

namespace Floris.Models
{
    public class User
    {
        public int Id { get; set; }
        
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        
        [Required]
        public string PasswordHash { get; set; }
        public string? Phone { get; set; }
        public string? FistName { get; set; }
        public string? LastName { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // навигационные свойства
        public ICollection<UserAddress> Addresses { get; set; } = new List<UserAddress>();
        public ICollection<Order> Orders { get; set; } = new List<Order>();
        public ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
    }
}
