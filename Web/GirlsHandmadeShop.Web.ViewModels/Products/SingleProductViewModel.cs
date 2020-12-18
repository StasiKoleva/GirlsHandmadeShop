namespace GirlsHandmadeShop.Web.ViewModels.Products
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using AutoMapper;
    using GirlsHandmadeShop.Data.Common.Models;
    using GirlsHandmadeShop.Data.Models;
    using GirlsHandmadeShop.Services.Mapping;

    public class SingleProductViewModel : IMapFrom<Product>, IHaveCustomMappings
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public string CategoryName { get; set; }

        public DateTime CreatedOn { get; set; }

        public string AddedByUserEmail { get; set; }

        public string ImageUrl { get; set; }

        public string Description { get; set; }

        public string Availability { get; set; }

        public int CategoryProductsCount { get; set; }

        public double AverageVote { get; set; }

        public IEnumerable<MaterialsViewModel> Materials { get; set; }

        public IEnumerable<ImagesViewModel> Images { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Product, SingleProductViewModel>()
                .ForMember(x => x.AverageVote, opt =>
                    opt.MapFrom(x => x.Votes.Count() == 0 ? 0 : x.Votes.Average(v => v.Value)))
                .ForMember(x => x.ImageUrl, opt =>
                    opt.MapFrom(x =>
                        "/images/products/" + x.Images.FirstOrDefault().Id + "." + x.Images.FirstOrDefault().Extension));
        }
    }
}
