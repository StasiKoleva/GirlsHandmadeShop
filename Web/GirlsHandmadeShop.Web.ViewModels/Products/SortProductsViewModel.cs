namespace GirlsHandmadeShop.Web.ViewModels.Products
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class SortProductsViewModel : ProductsListViewModel
    {
        public string NameSortAccending { get; set; }

        public string NameSortDescending { get; set; }

        public string PriceSortLowHigh { get; set; }

        public string PriceSortHighLow { get; set; }
    }
}
