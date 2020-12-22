namespace GirlsHandmadeShop.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using GirlsHandmadeShop.Common;
    using GirlsHandmadeShop.Data.Models;
    using GirlsHandmadeShop.Services.Data;
    using GirlsHandmadeShop.Web.ViewModels.Carts;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class CartsController : Controller
    {

        private readonly ICartsService cartsService;
        private readonly IProductsService productService;
        private readonly UserManager<ApplicationUser> userManager;

        public CartsController(
            ICartsService cartsService, IProductsService productService, UserManager<ApplicationUser> userManager)
        {
            this.cartsService = cartsService;
            this.userManager = userManager;
            this.productService = productService;
        }

        public IActionResult All()
        {
            var currentUserId = this.userManager.GetUserId(this.User);

            var productsInCart = this.cartsService.GetAllProductsFromCart<BaseProductInCartViewModel>(currentUserId);

            var viewModel = new ProductsListInCartViewModel
            {
                Products = productsInCart,
            };
            return this.View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> AddToCart(AddToCartInputModel input)
        {
            var user = await this.userManager.GetUserAsync(this.User);
            var productId = input.ProductId;
            var quantity = input.Quantity;

            await this.cartsService.AddToCartAsync(productId, user.Id, quantity);

            return this.RedirectToAction(nameof(this.All));
        }

        public async Task<IActionResult> Remove(string productId)
        {
            var user = await this.userManager.GetUserAsync(this.User);
            await this.cartsService.RemoveProductFromCart(user.Id, productId);

            return this.RedirectToAction(nameof(this.All));
        }
    }
}
