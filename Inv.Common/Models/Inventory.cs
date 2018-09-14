

namespace Inv.Common.Models
{
    using System.ComponentModel.DataAnnotations;
    public class Inventory
    {
        [Key]
        public int InventoryId { get; set; }
        public int ProductId { get; set; }
        public int LocationId { get; set; }
        public int BinId { get; set; }
        public int MeasureUnitId { get; set; }


    }
}
