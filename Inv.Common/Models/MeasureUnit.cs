namespace Inv.Common.Models
{
    using Newtonsoft.Json;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class MeasureUnit
    {
        [Key]
        [Display(Name = "Unit of Measure")]
        public int MeasureUnitId { get; set; }

        [Required]
        [StringLength(250)]
        [Display(Name = "U.M. Description")]
        public string Description { get; set; }

        [JsonIgnore]
        public virtual ICollection<Item> Items { get; set; }

        [JsonIgnore]
        public virtual ICollection<Count> Counts { get; set; }
    }
}
