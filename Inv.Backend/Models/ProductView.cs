﻿
namespace Inv.Backend.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.Web;
    using Common.Models;

    public class ProductView: Product
    {
        [Display(Name = "Image")]
        public HttpPostedFileBase ImageFile { get; set; }
    }
}