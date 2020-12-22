namespace GirlsHandmadeShop.Web.ViewModels.Home
{
    using GirlsHandmadeShop.Web.ViewModels.Carts;
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class IndexViewModel /*: BaseProductInCartViewModel*/
    {
        public int ProductsCount { get; set; }

        public int CategoriesCount { get; set; }

        public int ImagesCount { get; set; }

        public IEnumerable<IndexPageProductViewModel> RandomProducts { get; set; }
        public IEnumerable<BaseProductInCartViewModel> Products { get; set; }

        
    }
}
