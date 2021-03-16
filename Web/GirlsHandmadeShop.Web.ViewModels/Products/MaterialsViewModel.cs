namespace GirlsHandmadeShop.Web.ViewModels.Products
{
    using AutoMapper;
    using GirlsHandmadeShop.Data.Models;
    using GirlsHandmadeShop.Services.Mapping;

    public class MaterialsViewModel : IMapFrom<ProductMaterial>, IHaveCustomMappings
    {
        public string MaterialName { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<ProductMaterial, MaterialsViewModel>()
                .ForMember(x => x.MaterialName, opt =>
                    opt.MapFrom(x => x.Material.Name));
        }
    }
}