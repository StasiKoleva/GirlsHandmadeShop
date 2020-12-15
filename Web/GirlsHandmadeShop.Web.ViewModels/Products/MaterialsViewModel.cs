namespace GirlsHandmadeShop.Web.ViewModels.Products
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using GirlsHandmadeShop.Data.Models;
    using GirlsHandmadeShop.Services.Mapping;

    public class MaterialsViewModel : IMapFrom<ProductMaterial>
    {
        public string MaterialName { get; set; }
    }
}
