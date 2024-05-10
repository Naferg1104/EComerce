using EComerce.Api.Products.Models;

namespace EComerce.Api.Products.Interfaces
{
    public interface IProductsProvider
    {
        public Task<(bool isSuccess, IEnumerable<Product> products, string errorMessage)> GetProductsAsync();
        public Task<(bool isSuccess, Product product, string errorMessage)> GetProductAsync(int id);
    }
}
