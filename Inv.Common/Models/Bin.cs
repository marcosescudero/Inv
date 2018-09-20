
namespace Inv.Common.Models
{
    using Newtonsoft.Json;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Bin
    {
        [Key]
        public int BinId { get; set; }

        [Required]
        [StringLength(250)]
        [Display(Name = "Bin Description")]
        public string Description { get; set; }

        [Required]
        [Display(Name = "Location")]
        public int LocationId { get; set; }

        public virtual Location Location { get; set; }

        [JsonIgnore]
        public virtual ICollection<Count> Counts { get; set; }
    }
}
