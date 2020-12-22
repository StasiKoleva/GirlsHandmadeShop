namespace GirlsHandmadeShop.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;
    using GirlsHandmadeShop.Data.Models;
    using GirlsHandmadeShop.Web.ViewModels.Carts;

    public interface ICartsService
    {
        Task AddToCartAsync(string productId, string userId, int quantity = 1);

        IEnumerable<BaseProductInCartViewModel> GetAllProductsFromCart<BaseProductInCartViewModel>(string userId);

        bool IsProductAlreadyInCart(string productId, string userId);

        CartProducts GetProductFromCart(string productId, string userId);

        public Task<int> GetProductsCountInCart(string userId);

        public Task RemoveProductFromCart(string userId, string productId);
    }
}
