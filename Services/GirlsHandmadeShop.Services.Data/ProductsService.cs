namespace GirlsHandmadeShop.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using GirlsHandmadeShop.Data.Common.Models;
    using GirlsHandmadeShop.Data.Common.Repositories;
    using GirlsHandmadeShop.Data.Models;
    using GirlsHandmadeShop.Web.ViewModels.Products;

    public class ProductsService : IProductsService
    {
        private readonly string[] allowedExtensions = new[] { "jpg", "png", "gif" };
        private readonly IDeletableEntityRepository<Product> productsRepository;

        public ProductsService(IDeletableEntityRepository<Product> productsRepository)
        {
            this.productsRepository = productsRepository;
        }

        public async Task CreateAsync(CreateProductInputModel input, string userId, string imagePath)
        {
            var product = new Product
            {
                CategoryId = input.CategoryId,
                Description = input.Description,
                Name = input.Name,
                Price = input.Price,
                AddedByUserId = userId,
            };

            // /wwwroot/images/products/jhdsi-343g3h453-=g34g.jpg
            Directory.CreateDirectory($"{imagePath}/products/");
            foreach (var image in input.Images)
            {
                var extension = Path.GetExtension(image.FileName).TrimStart('.');
                if (!this.allowedExtensions.Any(x => extension.EndsWith(x)))
                {
                    throw new Exception($"Invalid image extension {extension}");
                }

                var dbImage = new Image
                {
                    AddedByUserId = userId,
                    Extension = extension,
                };
                product.Images.Add(dbImage);

                var physicalPath = $"{imagePath}/products/{dbImage.Id}.{extension}";
                using Stream fileStream = new FileStream(physicalPath, FileMode.Create);
                await image.CopyToAsync(fileStream);
            }

            await this.productsRepository.AddAsync(product);
            await this.productsRepository.SaveChangesAsync();
        }
    }
}
