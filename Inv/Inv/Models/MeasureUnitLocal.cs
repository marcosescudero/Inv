

namespace Inv.Models
{
    using SQLite;

    [Table("MeasureUnit")]
    public class MeasureUnitLocal
    {
        public int MeasureUnitId { get; set; }

        public string Description { get; set; }

    }
}
