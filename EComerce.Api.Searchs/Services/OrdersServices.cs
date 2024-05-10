using EComerce.Api.Searchs.Interfaces;
using EComerce.Api.Searchs.Models;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json;

namespace EComerce.Api.Searchs.Services
{
    public class OrdersServices : IOrdersService
    {
        private readonly IHttpClientFactory httpClientFactory;
        private readonly ILogger<OrdersServices> logger;
        public OrdersServices(IHttpClientFactory httpClientFactory, ILogger<OrdersServices> logger) { 

            this.httpClientFactory = httpClientFactory;
            this.logger = logger;
        }
         
        public async Task<(bool isSuccess, IEnumerable<Order> orders, string errorMessage)> GetOrdersAsync(int customerId)
        {
            try
            {
                var client = this.httpClientFactory.CreateClient("OrdersService");
                var response = await client.GetAsync($"api/orders/{customerId}");

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsByteArrayAsync();    
                    var options = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };
                    var result = JsonSerializer.Deserialize<IEnumerable<Order>>(content, options);

                    return (true, result, null);
                }

                return (false, null, response.ReasonPhrase);

            }catch (Exception ex)
            {
                logger?.LogError(ex.Message);
                return (false, null, ex.Message);
            }
        }
    }
}
