namespace GirlsHandmadeShop.Web.ViewComponents
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;

    using GirlsHandmadeShop.Services.Data;
    using GirlsHandmadeShop.Web.ViewModels.Carts;
    using Microsoft.AspNetCore.Mvc;

    public class CartBodyViewComponent : ViewComponent
    {
        private readonly ICartsService cartsService;

        public CartBodyViewComponent(
            ICartsService cartsService)
        {
            this.cartsService = cartsService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var userId = this.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var products = this.cartsService.GetAllProductsFromCart<BaseProductInCartViewModel>(userId);

           // products = products.AsEnumerable();
            var viewModel = new ProductsListInCartViewModel
            {
                Products = products ?? new List<BaseProductInCartViewModel>(),
            };
            return this.View(viewModel);
        }
    }
}
