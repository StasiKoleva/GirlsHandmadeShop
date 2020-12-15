namespace GirlsHandmadeShop.Web.ViewModels.Products
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    public class ProductMaterialInputModel
    {
        [Required]
        [MinLength(3)]
        public string MaterialName { get; set; }
    }
}
