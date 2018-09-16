
namespace Inv.Backend.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.Web;
    using Common.Models;

    public class ItemView :Item
    {
        [Display(Name = "Image")]
        public HttpPostedFileBase ImageFile { get; set; }
    }
}