using GirlsHandmadeShop.Common;
using GirlsHandmadeShop.Data.Models;
using GirlsHandmadeShop.Services.Data;
using GirlsHandmadeShop.Web.ViewModels.Carts;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GirlsHandmadeShop.Web.Controllers
{
    public class CartsController : Controller
    {

        private readonly ICartsService cartsService;
        private readonly IProductsService productService;
        private readonly UserManager<ApplicationUser> userManager;

        public CartsController(
            ICartsService cartsService, IProductsService productService, UserManager<ApplicationUser> userManager
            )
        {
            this.cartsService = cartsService;
            this.userManager = userManager;
            this.productService = productService;
        }

        public IActionResult All()
        {
            //var currentUserId = this.userService.GetUserId(this.User.Identity.Name);
            //var productsInCart = this.cartService.GetAllProductsFromCart(currentUserId);

            //var complexModel = new ComplexModel<List<BuyProductInputModel>, List<ProductCartViewModel>>
            //{
            //    ViewModel = productsInCart
            //};

            //if (TempData.ContainsKey(GlobalConstants.ErrorsFromPOSTRequest))
            //{
            //    //Merge model states
            //    ModelStateHelper.MergeModelStates(TempData, this.ModelState);
            //}

            //return this.View(complexModel);
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> AddToCart(AddToCartInputModel input)
        {
            var user = await this.userManager.GetUserAsync(this.User);
            var productId = input.ProductId;
            var quantity = input.Quantity;

            // var product = this.productService.GetById<AddToCartInputModel>(productId);


            await this.cartsService.AddToCartAsync(productId, user.Id, quantity);

            return this.RedirectToAction(nameof(this.All));

            //return this.View();
        }
    }
}
