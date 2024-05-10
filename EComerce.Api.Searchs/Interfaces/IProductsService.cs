using EComerce.Api.Searchs.Models;

namespace EComerce.Api.Searchs.Interfaces
{
    public interface IProductsService
    {
        public Task<(bool isSuccess, IEnumerable<Product> products, string errorMessage)> GetProductsAsync();
    }
}
