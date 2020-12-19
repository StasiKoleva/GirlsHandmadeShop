namespace GirlsHandmadeShop.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using GirlsHandmadeShop.Data.Common.Repositories;
    using GirlsHandmadeShop.Data.Models;
    using GirlsHandmadeShop.Services.Mapping;
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
            var cartProduct = new CartProducts { UserId = userId, ProductId = productId, Quantity = quantity };

            if (this.ProductIsInCart(productId, userId))
            {
                var productCart = this.GetProductFromCart(productId, userId);
                productCart.Quantity += quantity;
            }
            else
            {
                await this.cartsRepository.AddAsync(cartProduct);
            }

            await this.cartsRepository.SaveChangesAsync();
        }

        public bool ProductIsInCart(string productId, string userId)
        {
            return this.cartsRepository.All().Any(x => x.ProductId == productId && x.UserId == userId);
        }

        public CartProducts GetProductFromCart(string productId, string userId)
        {
            return this.cartsRepository.All().FirstOrDefault(x => x.ProductId == productId && x.UserId == userId);
        }

        public IEnumerable<BaseProductInCartViewModel> GetAllProductsFromCart<BaseProductInCartViewModel>(string userId)
        {
            //return this.cartsRepository.All().Where(x => x.UserId == userId).Select(x => new BaseProductInCartViewModel
            //{
            //    ImageUrl = x.Product.Images.First(),
            //    Name = x.Product.Name,
            //    Price = x.Product.Price,
            //    Id = x.Product.Id,
            //    Quantity = x.Quantity,
            //}).ToList();

             var productsInCart = this.cartsRepository.All().Where(x => x.UserId == userId).To<BaseProductInCartViewModel>().ToList();

            return productsInCart;
        }
    }
}
