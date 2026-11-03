namespace Floris.Models
{
    public class City
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Timezone { get; set; } = "Europe/Moscow";

        public ICollection<Store> Stores { get; set; } = new List<Store>();
    }
}
