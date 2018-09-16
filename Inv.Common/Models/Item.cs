namespace Inv.Common.Models
{
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

        [Display(Name = "Image")]
        public string ImagePath { get; set; }

        [Display(Name = "Is Available")]
        public bool IsAvailable { get; set; }

        [NotMapped] // Cuando tengo atributos que forman parte del modelo, pero quiero que NO formen parte de la base de datos, se coloca [NotMapped]
        public byte[] ImageArray { get; set; }

        public string ImageFullPath
        {
            get
            {
                if (string.IsNullOrEmpty(this.ImagePath))
                {
                    return "noimage";
                }
                //return $"http://200.55.241.235/InvBackend{this.ImagePath.Substring(1)}"; // el substring es para quitarle el ñuflo
                return $"http://200.55.241.235/InvAPI{this.ImagePath.Substring(1)}"; // el substring es para quitarle el ñuflo
            }
        }

        public override string ToString()
        {
            return this.Description;
        }

        public virtual MeasureUnit MeasureUnit { get; set; }
               
        public virtual ICollection<Count> Counts { get; set; }

    }
}
