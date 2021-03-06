﻿namespace GirlsHandmadeShop.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using GirlsHandmadeShop.Common;
    using GirlsHandmadeShop.Data;
    using GirlsHandmadeShop.Data.Models;
    using GirlsHandmadeShop.Services.Data;
    using GirlsHandmadeShop.Services.Messaging;
    using GirlsHandmadeShop.Web.ViewModels.Products;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class ProductsController : Controller
    {
        private readonly ICategoriesService categoriesService;
        private readonly IProductsService productsService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IWebHostEnvironment environment;
        private readonly IEmailSender emailSender;

        public ProductsController(
            ICategoriesService categoriesService,
            IProductsService productsService,
            UserManager<ApplicationUser> userManager,
            IWebHostEnvironment environment,
            IEmailSender emailSender)
        {
            this.categoriesService = categoriesService;
            this.productsService = productsService;
            this.userManager = userManager;
            this.environment = environment;
            this.emailSender = emailSender;
        }

        [Authorize]
        public IActionResult Create()
        {
            var viewModel = new CreateProductInputModel();
            viewModel.CategoriesItems = this.categoriesService.GetAllAsKeyValuePairs();
            return this.View(viewModel);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(CreateProductInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                input.CategoriesItems = this.categoriesService.GetAllAsKeyValuePairs();
                return this.View(input);
            }

            // var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var user = await this.userManager.GetUserAsync(this.User);

            try
            {
                await this.productsService.CreateAsync(input, user.Id, $"{this.environment.WebRootPath}/images");
            }
            catch (Exception ex)
            {
                this.ModelState.AddModelError(string.Empty, ex.Message);
                input.CategoriesItems = this.categoriesService.GetAllAsKeyValuePairs();
                return this.View(input);
            }

            this.TempData["Message"] = "Product added successfully.";

            // TODO: Redirect to product info page
            return this.RedirectToAction("Create");
        }

        public IActionResult All(int id = 1)
        {
            if (id <= 0)
            {
                return this.NotFound();
            }

            const int ItemsPerPage = 12;
            var viewModel = new ProductsListViewModel
            {
                ItemsPerPage = ItemsPerPage,
                PageNumber = id,
                ProductsCount = this.productsService.GetCount(),
                Products = this.productsService.GetAll<ProductInListViewModel>(id, ItemsPerPage),
            };
            return this.View(viewModel);
        }

        public IActionResult ById(string id)
        {
            var product = this.productsService.GetById<SingleProductViewModel>(id);
            return this.View(product);
        }


        public IActionResult Sort(int sorting)
        {
            // var product = this.productsService.Sort(sorting);

            var viewModel = new ProductsListViewModel
            {
                ItemsPerPage = 12,
                PageNumber = 1,
                ProductsCount = this.productsService.GetCount(),
                Products = this.productsService.Sort<ProductsListViewModel>(sorting),
            };
            return this.View("All", viewModel);
        }

        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public IActionResult Edit(string id)
        {
            var inputModel = this.productsService.GetById<EditProductInputModel>(id);
            inputModel.CategoriesItems = this.categoriesService.GetAllAsKeyValuePairs();
            return this.View(inputModel);
        }

        [HttpPost]
        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public async Task<IActionResult> Edit(string id, EditProductInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                input.CategoriesItems = this.categoriesService.GetAllAsKeyValuePairs();
                return this.View(input);
            }

            await this.productsService.UpdateAsync(id, input);
            return this.RedirectToAction(nameof(this.ById), new { id });
        }

        [HttpPost]
        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public async Task<IActionResult> Delete(string id)
        {
            await this.productsService.DeleteAsync(id);
            return this.RedirectToAction(nameof(this.All));
        }
    }
}
