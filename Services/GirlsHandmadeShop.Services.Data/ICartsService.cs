namespace GirlsHandmadeShop.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    using GirlsHandmadeShop.Web.ViewModels.Carts;

    public interface ICartsService
    {
        Task AddToCartAsync(string productId, string userId, int quantity = 1);
    }
}
