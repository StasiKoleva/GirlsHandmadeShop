namespace GirlsHandmadeShop.Data.Seeding
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using GirlsHandmadeShop.Data.Models;

    public class CategoriesSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Categories.Any())
            {
                return;
            }

            await dbContext.Categories.AddAsync(new Category { Name = "Necklaces" });
            await dbContext.Categories.AddAsync(new Category { Name = "Bracelets" });
            await dbContext.Categories.AddAsync(new Category { Name = "Earrings" });
            await dbContext.Categories.AddAsync(new Category { Name = "Accessories" });

            await dbContext.SaveChangesAsync();
        }
    }
}
