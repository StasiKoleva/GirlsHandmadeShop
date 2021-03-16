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

        public T GetById<T>(string id)
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

        public async Task UpdateAsync(string id, EditProductInputModel input)
        {
            var products = this.productsRepository.All().FirstOrDefault(x => x.Id == id);
            products.Name = input.Name;
            products.Description = input.Description;
            products.Price = input.Price;
            products.Availability = input.Availability;
            products.CategoryId = input.CategoryId;
            await this.productsRepository.SaveChangesAsync();
        }

        public async Task DeleteAsync(string id)
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

               // 10 ->$0.00 - $20.00
               // 11 -> $21.00 - $40.00
               // 12 -> $41.00 - $60.00
               // 13 -> $61.00 - $80.00
               // 14 -> $81.00 - $100.00
               // 15 -> $100.00+

               // 16 -> Swarovski pearls
               // 17 -> Swarovski crystals
               // 18 -> Natural stones
               // 19 -> Japanese beads
               // 20 -> Czech beads
               // 21 -> Silver hooks


               1 => result.OrderBy(x => x.Name).ToList(),
               2 => result.OrderBy(x => x.Name).ToList(),
               3 => result.OrderByDescending(x => x.Name).ToList(),
               4 => result.OrderBy(x => x.Price).ToList(),
               5 => result.OrderByDescending(x => x.Price).ToList(),
               6 => result.OrderBy(x => x.Name).Where(x => x.CategoryName == "Necklaces").ToList(),
               7 => result.OrderBy(x => x.Name).Where(x => x.CategoryName == "Bracelets").ToList(),
               8 => result.OrderBy(x => x.Name).Where(x => x.CategoryName == "Earrings").ToList(),
               9 => result.OrderBy(x => x.Name).Where(x => x.CategoryName == "Accessories").ToList(),
               10 => result.OrderBy(x => x.Price).Where(x => x.Price >= 0 && x.Price <= 20).ToList(),
               11 => result.OrderBy(x => x.Price).Where(x => x.Price >= 21 && x.Price <= 40).ToList(),
               12 => result.OrderBy(x => x.Price).Where(x => x.Price >= 41 && x.Price <= 60).ToList(),
               13 => result.OrderBy(x => x.Price).Where(x => x.Price >= 61 && x.Price <= 80).ToList(),
               14 => result.OrderBy(x => x.Price).Where(x => x.Price >= 81 && x.Price <= 100).ToList(),
               15 => result.OrderBy(x => x.Price).Where(x => x.Price >= 101).ToList(),
               //16 => result.OrderBy(x => x.Materials.Any(x => x.Name == "Swarovski pearls")).ToList(),
               //17 => result.OrderBy(x => x.Materials.Any(x => x.Name == "Swarovski crystals")).ToList(),
               //18 => result.OrderBy(x => x.Materials.Any(x => x.Name == "Natural stones")).ToList(),
               //19 => result.OrderBy(x => x.Materials.Any(x => x.Name == "Japanese beads")).ToList(),
               //20 => result.OrderBy(x => x.Name).Where(x => x.CategoryName == "Czech beads").ToList(),
               //21 => result.OrderBy(x => x.Materials.Any(x => x.Name == "Silver hooks")).ToList(),
               _ => result.OrderByDescending(x => x.Name).ToList(),
           };
    }
}
