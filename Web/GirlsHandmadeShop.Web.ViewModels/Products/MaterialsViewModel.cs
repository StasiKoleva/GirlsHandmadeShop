namespace GirlsHandmadeShop.Web.ViewModels.Products
{
    using GirlsHandmadeShop.Data.Models;
    using GirlsHandmadeShop.Services.Mapping;

    public class MaterialsViewModel : IMapFrom<ProductMaterial>
    {
        public string MaterialName { get; set; }
    }
}
