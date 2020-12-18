namespace GirlsHandmadeShop.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using GirlsHandmadeShop.Data.Common.Repositories;
    using GirlsHandmadeShop.Data.Models;
    using GirlsHandmadeShop.Web.ViewModels.Carts;

    public class CartsService : ICartsService
    {
        private readonly IDeletableEntityRepository<Product> productsRepository;
        private readonly IRepository<CartProducts> cartsRepository;
        private readonly IDeletableEntityRepository<ApplicationUser> usersRepository;

        public CartsService(IDeletableEntityRepository<Product> productsRepository, IRepository<CartProducts> cartsRepository, IDeletableEntityRepository<ApplicationUser> usersRepository)
        {
            this.productsRepository = productsRepository;
            this.cartsRepository = cartsRepository;
            this.usersRepository = usersRepository;
        }

        public async Task AddToCartAsync(string productId, string userId, int quantity = 1)
        {

            var currentUser = this.usersRepository.All().FirstOrDefault(x => x.Id == userId);
            var currentUserCart = currentUser.Cart;
            var product = this.productsRepository.All().FirstOrDefault(x => x.Id == productId);

            // var cartId = currentUser.Carts.
            //    Add(new Cart { Quantity = 1, TotalPrice = 5.98m, UserId = currentUser.Id, CartProducts = new List<CartProducts> }).;

            // var cartProduct = new CartProducts { CartId = "15c97b51-faa5-4422-96f7-c76528e44162", ProductId = product.Id, Quantity = quantity };

            var cartProduct = new CartProducts { CartId = currentUserCart.Id, ProductId = product.Id, Quantity = 1 };

            //currentUserCart.CartProducts.Add(cartProduct);

            await this.cartsRepository.AddAsync(cartProduct);

            await this.cartsRepository.SaveChangesAsync();

            // if (this.ProductIsInCart(productId, userId))
            // {
            //    var productCart = this.GetProductFromCart(productId, userId);
            //    productCart.Quantity += quantity;
            // }
            // else
            // {
            //    await this.context.Carts.AddAsync(new ProductCart { UserId = userId, ProductId = productId, Quantity = quantity });
            // }
            // await this.context.SaveChangesAsync();
        }
    }
}
