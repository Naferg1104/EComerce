using EComerce.Api.Searchs.Interfaces;
using EComerce.Api.Searchs.Models;
using System.Text.Json;

namespace EComerce.Api.Searchs.Services
{
    public class ProductsService : IProductsService
    {

        private readonly IHttpClientFactory httpClientFactory;
        private readonly ILogger<OrdersServices> logger;
        public ProductsService(IHttpClientFactory httpClientFactory, ILogger<OrdersServices> logger)
        {
            this.httpClientFactory = httpClientFactory;
            this.logger = logger;
        }

        public async Task<(bool isSuccess, IEnumerable<Product> products, string errorMessage)> GetProductsAsync()
        {
            try
            {
                var client = this.httpClientFactory.CreateClient("ProductsService");
                var response = await client.GetAsync($"api/products");

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsByteArrayAsync();
                    var options = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };
                    var result = JsonSerializer.Deserialize<IEnumerable<Product>>(content, options);

                    return (true, result, null);
                }

                return (false, null, response.ReasonPhrase);

            }
            catch (Exception ex)
            {
                logger?.LogError(ex.ToString());
                return (false, null, ex.Message);
            }
        }
    }
}
