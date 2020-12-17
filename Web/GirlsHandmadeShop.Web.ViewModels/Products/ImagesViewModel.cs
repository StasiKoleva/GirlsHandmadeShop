namespace GirlsHandmadeShop.Web.ViewModels.Products
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using AutoMapper;
    using GirlsHandmadeShop.Data.Common.Models;
    using GirlsHandmadeShop.Services.Mapping;

    public class ImagesViewModel : IMapFrom<Image>, IHaveCustomMappings
    {
        public string ImageUrl { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Image, ImagesViewModel>()
                .ForMember(x => x.ImageUrl, opt =>
                    opt.MapFrom(x =>
                        "/images/products/" + x.Id + "." + x.Extension));
        }
    }
}
