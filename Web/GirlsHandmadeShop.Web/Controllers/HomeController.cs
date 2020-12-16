namespace GirlsHandmadeShop.Web.Controllers
{
    using System.Diagnostics;
    using System.Linq;

    using GirlsHandmadeShop.Data;
    using GirlsHandmadeShop.Services.Data;
    using GirlsHandmadeShop.Web.ViewModels;
    using GirlsHandmadeShop.Web.ViewModels.Home;
    using Microsoft.AspNetCore.Mvc;

    public class HomeController : BaseController
    {
        private readonly IGetCountsService countsService;
        private readonly IProductsService productsService;

        public HomeController(IGetCountsService countsService, IProductsService productsService)
        {
            this.countsService = countsService;
            this.productsService = productsService;
        }

        public IActionResult Index()
        {
            var countsDto = this.countsService.GetCounts();

            var viewModel = new IndexViewModel
            {
                ProductsCount = countsDto.ProductsCount,
                CategoriesCount = countsDto.CategoriesCount,
                ImagesCount = countsDto.ImagesCount,
                RandomProducts = this.productsService.GetRandom<IndexPageProductViewModel>(4),
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
