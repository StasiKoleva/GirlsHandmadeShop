namespace GirlsHandmadeShop.Web.ViewModels.Home
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class IndexViewModel
    {
        public int ProductsCount { get; set; }

        public int CategoriesCount { get; set; }

        public int ImagesCount { get; set; }

        public IEnumerable<IndexPageProductViewModel> RandomProducts { get; set; }
    }
}
