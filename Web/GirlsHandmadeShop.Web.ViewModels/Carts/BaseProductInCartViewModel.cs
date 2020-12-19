namespace GirlsHandmadeShop.Web.ViewModels.Carts
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using AutoMapper;
    using GirlsHandmadeShop.Data.Common.Models;
    using GirlsHandmadeShop.Data.Models;
    using GirlsHandmadeShop.Services.Mapping;

    public class BaseProductInCartViewModel : IMapFrom<CartProducts>, IHaveCustomMappings
    {
       // public string Id { get; set; }

        public string ImageUrl { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

       // public string UserId { get; set; }

       // public string ProductId { get; set; }

        public int Quantity { get; set; }

        public decimal TotalProductsPrice => this.Quantity * this.Price;

       // public decimal TotalCartPrice { get { return this.Sum(this.TotalProductsPrice); } }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<CartProducts, BaseProductInCartViewModel>()
                .ForMember(x => x.ImageUrl, opt =>
                    opt.MapFrom(x =>
                        "/images/products/" + x.Product.Images.FirstOrDefault().Id + "." + x.Product.Images.FirstOrDefault().Extension))
            .ForMember(x => x.Name, opt =>
                    opt.MapFrom(x => x.Product.Name))
            .ForMember(x => x.Price, opt =>
                    opt.MapFrom(x => x.Product.Price));
        }
    }
}