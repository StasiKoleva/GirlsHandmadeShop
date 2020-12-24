namespace GirlsHandmadeShop.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using GirlsHandmadeShop.Data.Models;
    using GirlsHandmadeShop.Services.Data;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class OrdersController : Controller
    {

        private readonly ICartsService cartsService;
        private readonly IProductsService productService;
        private readonly UserManager<ApplicationUser> userManager;

        public OrdersController(
            ICartsService cartsService, IProductsService productService, UserManager<ApplicationUser> userManager)
        {
            this.cartsService = cartsService;
            this.userManager = userManager;
            this.productService = productService;
        }

        public IActionResult ViewOrder()
        {
            //var currentUserId = this.userManager.GetUserId(this.User);

            //var productsInCart = this.cartsService.GetAllProductsFromCart<BaseProductInCartViewModel>(currentUserId);

            //var viewModel = new ProductsListInCartViewModel
            //{
            //    Products = productsInCart,
            //};
            //return this.View(viewModel);
            return this.View();
        }
    }
}
