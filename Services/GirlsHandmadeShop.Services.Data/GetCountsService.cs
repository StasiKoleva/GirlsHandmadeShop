namespace GirlsHandmadeShop.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using GirlsHandmadeShop.Data.Common.Models;
    using GirlsHandmadeShop.Data.Common.Repositories;
    using GirlsHandmadeShop.Data.Models;
    using GirlsHandmadeShop.Services.Data.DTOs;

    public class GetCountsService : IGetCountsService
    {
        private readonly IDeletableEntityRepository<Category> categoriesRepository;
        private readonly IRepository<Image> imagesRepository;
        private readonly IDeletableEntityRepository<Product> productsRepository;

        public GetCountsService(
            IDeletableEntityRepository<Category> categoriesRepository,
            IRepository<Image> imagesRepository,
            IDeletableEntityRepository<Product> productsRepository)
        {
            this.categoriesRepository = categoriesRepository;
            this.imagesRepository = imagesRepository;
            this.productsRepository = productsRepository;
        }

        public CountsDto GetCounts()
        {
            var data = new CountsDto
            {
                CategoriesCount = this.categoriesRepository.All().Count(),
                ImagesCount = this.imagesRepository.All().Count(),
                ProductsCount = this.productsRepository.All().Count(),
            };

            return data;
        }
    }
}
