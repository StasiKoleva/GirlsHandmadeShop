namespace GirlsHandmadeShop.Web.ViewModels.Carts
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    using GirlsHandmadeShop.Data.Models;
    using GirlsHandmadeShop.Services.Mapping;

    public class AddToCartInputModel /*: IMapFrom<CartProducts>*/
    {
        [Required]
        public string ProductId { get; set; }

        [Range(1, 2147483647, ErrorMessage = "Cannot buy this many products.")]
        public int Quantity { get; set; }
    }
}
