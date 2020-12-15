namespace GirlsHandmadeShop.Web.ViewModels.Products
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using Microsoft.AspNetCore.Http;

    public class CreateProductInputModel : BaseProductInputModel
    {
        public IEnumerable<IFormFile> Images { get; set; }

        public IEnumerable<ProductMaterialInputModel> Materials { get; set; }
    }
}
