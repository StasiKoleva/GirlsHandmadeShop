namespace GirlsHandmadeShop.Web.ViewModels.Products
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using AutoMapper;
    using GirlsHandmadeShop.Data.Models;
    using GirlsHandmadeShop.Services.Mapping;

    public class EditProductInputModel : BaseProductInputModel, IMapFrom<Product>
    {
        public int Id { get; set; }
    }
}
