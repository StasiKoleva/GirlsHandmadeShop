namespace GirlsHandmadeShop.Web.ViewModels.Products
{
    using System.Collections.Generic;
    using System.Linq;

    using AutoMapper;
    using GirlsHandmadeShop.Data.Common.Models;
    using GirlsHandmadeShop.Data.Models;
    using GirlsHandmadeShop.Services.Mapping;

    public class ProductInListViewModel : IMapFrom<Product>, IHaveCustomMappings
    {
        public string Id { get; set; }

        public string ImageUrl { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public string Description { get; set; }

        public string Availability { get; set; }

        public int CategoryId { get; set; }

        public int QuantityInStock { get; set; }

        public string CategoryName { get; set; }

        public virtual ICollection<Image> Images { get; set; }

        //public virtual ICollection<Material> Materials { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Product, ProductInListViewModel>()
                .ForMember(x => x.ImageUrl, opt =>
                    opt.MapFrom(x =>
                        "/images/products/" + x.Images.FirstOrDefault().Id + "." + x.Images.FirstOrDefault().Extension))

                //.ForMember(x => x.Materials., opt =>
                //     opt.MapFrom(x =>
                //     x.Materials))
                ;
        }
    }
}
