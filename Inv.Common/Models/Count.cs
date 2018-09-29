using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inv.Common.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    public class Count
    {
        [Key]
        public int CountId { get; set; }

        [Display(Name = "Item")]
        public int? ItemId { get; set; }

        [Display(Name = "Location")]
        public int? LocationId { get; set; }

        [Display(Name = "U.M.")]
        public int? MeasureUnitId { get; set; }

        public decimal Quantity { get; set; }

        [Display(Name = "Count Date")]
        [DataType(DataType.Date)]
        public DateTime CountDate { get; set; }

        public virtual Item Item { get; set; }
        public virtual MeasureUnit MeasureUnit { get; set; }
        public virtual Location Location { get; set; }

    }
}