namespace GirlsHandmadeShop.Web.ViewModels.Carts
{
    using System.Collections.Generic;
    using System.Linq;

    public class ProductsListInCartViewModel
    {
        public IEnumerable<BaseProductInCartViewModel> Products { get; set; }

        public int ProductsCount => this.Products.Count();

        public decimal GrandTotalPrice => this.Products.Sum(x => x.TotalProductsPrice);
    }
}
