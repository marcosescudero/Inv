namespace Inv.Models
{
    using SQLite;

    [Table("Location")]
    public class LocationLocal
    {
        public int LocationId { get; set; }
        public string Description { get; set; }
    }
}
