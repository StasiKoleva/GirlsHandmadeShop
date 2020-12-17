namespace GirlsHandmadeShop.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    using GirlsHandmadeShop.Data.Common.Models;
    using GirlsHandmadeShop.Data.Common.Repositories;
    using GirlsHandmadeShop.Data.Models;
    using GirlsHandmadeShop.Services.Mapping;
    using GirlsHandmadeShop.Web.ViewModels.Products;

    public class ProductsService : IProductsService
    {
        private readonly string[] allowedExtensions = new[] { "jpg", "png", "gif" };
        private readonly IDeletableEntityRepository<Product> productsRepository;
        private readonly IDeletableEntityRepository<Material> materialsRepository;

        public ProductsService(IDeletableEntityRepository<Product> productsRepository, IDeletableEntityRepository<Material> materialsRepository)
        {
            this.productsRepository = productsRepository;
            this.materialsRepository = materialsRepository;
        }

        public IEnumerable<ProductInListViewModel> Sort<TProductsInListViewModel>(int sorting)
        {
            var products = this.productsRepository.AllAsNoTracking().ToList();
            var result = new List<ProductInListViewModel>();

            foreach (var item in products)
            {
                var imageUrl = this.productsRepository.AllAsNoTracking()
                    .Where(x => x.Id == item.Id).OrderBy(x => x.Name)
                    .Select(x => x.Images)
                    .FirstOrDefault();

                var product = this.productsRepository.AllAsNoTracking()
                 .Where(x => x.Id == item.Id)
                 .To<ProductInListViewModel>().FirstOrDefault();

                result.Add(product);
            }

            return this.SortProducts(result, sorting);
        }

        public async Task CreateAsync(CreateProductInputModel input, string userId, string imagePath)
        {
            var product = new Product
            {
                CategoryId = input.CategoryId,
                Description = input.Description,
                Name = input.Name,
                Price = input.Price,
                Availability = input.Availability,
                AddedByUserId = userId,
            };

            foreach (var inputIngredient in input.Materials)
            {
                var material = this.materialsRepository.All().FirstOrDefault(x => x.Name == inputIngredient.MaterialName);
                if (material == null)
                {
                    material = new Material { Name = inputIngredient.MaterialName };
                }

                product.Materials.Add(new ProductMaterial
                {
                    Material = material,
                });
            }

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

        public IEnumerable<T> GetAll<T>(int page, int itemsPerPage = 12)
        {
            var products = this.productsRepository.AllAsNoTracking()
                .OrderByDescending(x => x.Id)
                .Skip((page - 1) * itemsPerPage).Take(itemsPerPage)
                .To<T>().ToList();
            return products;
        }

        public int GetCount()
        {
            return this.productsRepository.All().Count();
        }

        public T GetById<T>(int id)
        {
            var product = this.productsRepository.AllAsNoTracking()
                .Where(x => x.Id == id)
                .To<T>().FirstOrDefault();

            return product;
        }

        public IEnumerable<T> GetByMaterials<T>(IEnumerable<int> materialIds)
        {
            var query = this.productsRepository.All().AsQueryable();
            foreach (var materialId in materialIds)
            {
                query = query.Where(x => x.Materials.Any(i => i.MaterialId == materialId));
            }

            return query.To<T>().ToList();
        }

        public IEnumerable<T> GetRandom<T>(int count)
        {
            return this.productsRepository.All()
                .OrderBy(x => Guid.NewGuid())
                .Take(count)
                .To<T>().ToList();
        }

        public async Task UpdateAsync(int id, EditProductInputModel input)
        {
            var products = this.productsRepository.All().FirstOrDefault(x => x.Id == id);
            products.Name = input.Name;
            products.Description = input.Description;
            products.Price = input.Price;
            products.Availability = input.Availability;
            products.CategoryId = input.CategoryId;
            await this.productsRepository.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var recipe = this.productsRepository.All().FirstOrDefault(x => x.Id == id);
            this.productsRepository.Delete(recipe);
            await this.productsRepository.SaveChangesAsync();
        }

        private IEnumerable<ProductInListViewModel> SortProducts(IEnumerable<ProductInListViewModel> result, int sorting) =>
           sorting switch
           {

               // 1 -> A-Z default
               // 2 -> A-Z
               // 3 -> Z-A
               // 4 ->Price low to high
               // 5 ->Price high to low
               // 6 -> Necklaces
               // 7 -> Bracelets
               // 8 -> Earrings
               // 9 -> Accesories
               1 => result.OrderBy(x => x.Name).ToList(),
               2 => result.OrderBy(x => x.Name).ToList(),
               3 => result.OrderByDescending(x => x.Price).ToList(),
               4 => result.OrderBy(x => x.Price).ToList(),
               5 => result.OrderBy(x => x.Price).ToList(),
               6 => result.OrderBy(x => x.Name).Where(x => x.CategoryName == "Necklaces").ToList(),
               7 => result.OrderBy(x => x.Name).Where(x => x.CategoryName == "Bracelets").ToList(),
               8 => result.OrderBy(x => x.Name).Where(x => x.CategoryName == "Earrings").ToList(),
               9 => result.OrderBy(x => x.Name).Where(x => x.CategoryName == "Accessories").ToList(),
               _ => result.OrderByDescending(x => x.Name).ToList(),
           };
    }
}
