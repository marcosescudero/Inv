namespace Inv.Common.Models
{
    using Newtonsoft.Json;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Item
    {
        [Key]
        public int ItemId { get; set; }

        [StringLength(50)]
        public string Barcode { get; set; }

        [Required]
        [StringLength(250)]
        public string Description { get; set; }

        [Required]
        [Display(Name = "Unit of Measure")]
        public int MeasureUnitId { get; set; }

        [Display(Name = "Is Available")]
        public bool IsAvailable { get; set; }

        public override string ToString()
        {
            return this.Description;
        }

        public virtual MeasureUnit MeasureUnit { get; set; }

        [JsonIgnore]
        public virtual ICollection<Count> Counts { get; set; }

    }
}
