namespace GirlsHandmadeShop.Web.Controllers
{
    using System.Diagnostics;
    using System.Linq;

    using GirlsHandmadeShop.Data;
    using GirlsHandmadeShop.Web.ViewModels;
    using GirlsHandmadeShop.Web.ViewModels.Home;
    using Microsoft.AspNetCore.Mvc;

    public class HomeController : BaseController
    {
        private readonly ApplicationDbContext db;

        public HomeController(ApplicationDbContext db)
        {
                this.db = db;
        }

        public IActionResult Index()
        {
            var viewModel = new IndexViewModel
            {
                ProductsCount = this.db.Products.Count(),

                CategoriesCount = this.db.Categories.Count(),

                ImagesCount = this.db.Images.Count(),
            };
            return this.View(viewModel);
        }

        public IActionResult Privacy()
        {
            return this.View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return this.View(
                new ErrorViewModel { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier });
        }
    }
}
