namespace Floris.Models
{
    public class PhotoReport
    {
        public int Id { get; set; }

        public int OrderId { get; set; }
        public Order Order { get; set; } = null!;

        public string Url { get; set; } = string.Empty;
        public string Type { get; set; } = "final"; // "final", "detail", "packaging"

        public int UploadedByUserId { get; set; }
        public User UploadedByUser { get; set; } = null!;

        public DateTime UploadedAt { get; set; } = DateTime.UtcNow;
    }
}