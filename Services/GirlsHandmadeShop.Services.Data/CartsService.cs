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

            if (this.IsProductAlreadyInCart(productId, userId))
            {
                var productCart = this.GetProductFromCart(productId, userId);
                productCart.Quantity += quantity;
            }
            else
            {
                var cartProduct = new CartProducts { UserId = userId, ProductId = productId, Quantity = quantity };

                await this.cartsRepository.AddAsync(cartProduct);
            }

            await this.cartsRepository.SaveChangesAsync();
        }



        public bool IsProductAlreadyInCart(string productId, string userId)
        {
            return this.cartsRepository.All().Any(x => x.ProductId == productId && x.UserId == userId);
        }

        public CartProducts GetProductFromCart(string productId, string userId)
        {
            return this.cartsRepository.All().FirstOrDefault(x => x.ProductId == productId && x.UserId == userId);
        }

        public IEnumerable<BaseProductInCartViewModel> GetAllProductsFromCart<BaseProductInCartViewModel>(string userId)
        {
            var productsInCart = this.cartsRepository.All()
                .Where(x => x.UserId == userId)
                .To<BaseProductInCartViewModel>()
                .ToList();

            return productsInCart;
        }

        public async Task<int> GetProductsCountInCart(string userId)
        {
            var productsCount = this.cartsRepository.All()
               .Select(x => new
               {
                   UserId = x.UserId,
                   Product = x.Product,
               })
               .Where(x => x.UserId == userId && x.Product.IsDeleted == false)
               .Count();

            return productsCount;
        }

        public async Task RemoveProductFromCart(string userId, string productId)
        {
            this.cartsRepository.Delete(this.GetProductFromCart(productId, userId));
            await this.cartsRepository.SaveChangesAsync();
        }
    }
}
