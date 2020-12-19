namespace GirlsHandmadeShop.Web.ViewModels.Carts
{
    using GirlsHandmadeShop.Data.Models;
    using GirlsHandmadeShop.Services.Mapping;
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class ProductsListInCartViewModel /*: IMapFrom<CartProducts>*/
    {
        public IEnumerable<BaseProductInCartViewModel> Products { get; set; }
    }
}
