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
            var cartProduct = new CartProducts { UserId = userId, ProductId = productId, Quantity = quantity };

            await this.cartsRepository.AddAsync(cartProduct);

            await this.cartsRepository.SaveChangesAsync();
        }
    }
}
