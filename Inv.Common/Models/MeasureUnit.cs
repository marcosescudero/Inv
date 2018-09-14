namespace Inv.Common.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class MeasureUnit
    {
        [Key]
        [Display(Name = "Unit of Measure")]
        public int MeasureUnitId { get; set; }

        [Required]
        [StringLength(250)]
        [Display(Name = "U.M. Description")]
        public string Description { get; set; }

        
        public virtual ICollection<Product> Products { get; set; }
    }
}
