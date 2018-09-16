namespace Inv.Common.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    public class Location
    {
        [Key]
        public int LocationId { get; set; }

        [Required]
        [StringLength(250)]
        public string Description { get; set; }

        public virtual ICollection<Bin> Bins { get; set; }

        //public virtual ICollection<Count> Counts { get; set; }
    }
}
