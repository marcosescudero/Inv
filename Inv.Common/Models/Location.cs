namespace Inv.Common.Models
{
    using Newtonsoft.Json;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    public class Location
    {
        [Key]
        public int LocationId { get; set; }

        [Required]
        [StringLength(250)]
        [Display(Name = "Location Description")]
        public string Description { get; set; }

        [JsonIgnore]
        public virtual ICollection<Count> Counts { get; set; }
    }
}
