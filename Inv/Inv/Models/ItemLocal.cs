namespace Inv.Models
{
    using Common.Models;
    using SQLite;

    [Table("Item")]
    public class ItemLocal
    {
        public int ItemId { get; set; }

        public string Barcode { get; set; }

        public string Description { get; set; }

        public int MeasureUnitId { get; set; }

        public bool IsAvailable { get; set; }
    }
}
