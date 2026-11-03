namespace Floris.Models
{
    public class Role
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty; // "client", "florist", "courier", "admin"

        public ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();

    }
}
