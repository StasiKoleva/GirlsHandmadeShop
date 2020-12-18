namespace GirlsHandmadeShop.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    using GirlsHandmadeShop.Web.ViewModels.Products;

    public interface IProductsService
    {
        Task CreateAsync(CreateProductInputModel input, string userId, string imagePath);

        IEnumerable<T> GetAll<T>(int page, int itemsPerPage = 12);

        IEnumerable<T> GetByMaterials<T>(IEnumerable<int> materials);

        int GetCount();

        T GetById<T>(string id);

        IEnumerable<T> GetRandom<T>(int count);

        public IEnumerable<ProductInListViewModel> Sort<TProductsInListViewModel>(int sorting);

        Task UpdateAsync(string id, EditProductInputModel input);

        Task DeleteAsync(string id);
    }
}
