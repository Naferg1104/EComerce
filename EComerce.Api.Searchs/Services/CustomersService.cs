using EComerce.Api.Searchs.Interfaces;
using EComerce.Api.Searchs.Models;
using System.Text.Json;

namespace EComerce.Api.Searchs.Services
{
    public class CustomersService : ICustomersService
    {

        private readonly IHttpClientFactory httpClientFactory;
        private readonly ILogger<CustomersService> logger;
        public CustomersService(IHttpClientFactory httpClientFactory, ILogger<CustomersService> logger)
        {

            this.httpClientFactory = httpClientFactory;
            this.logger = logger;
        }

        public async Task<(bool isSuccess, IEnumerable<Customer> customers, string errorMessage)> GetCustomersAsync()
        {
            try
            {
                var client = this.httpClientFactory.CreateClient("CustomersService");
                var response = await client.GetAsync($"api/customers");

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsByteArrayAsync();
                    var options = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };
                    var result = JsonSerializer.Deserialize<IEnumerable<Customer>>(content, options);

                    return (true, result, null);
                }

                return (false, null, response.ReasonPhrase);

            }
            catch (Exception ex)
            {
                logger?.LogError(ex.Message);
                return (false, null, ex.Message);
            }
        }
    }
}
