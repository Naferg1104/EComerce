using EComerce.Api.Searchs.Interfaces;

namespace EComerce.Api.Searchs.Services
{
    public class SearchService : ISearchService
    {
        private readonly IOrdersService ordersService;
        private readonly IProductsService productsService;
        private readonly ICustomersService customersService;

        public SearchService(IOrdersService ordersService, IProductsService productsService, ICustomersService customersService) {
            this.ordersService = ordersService;
            this.productsService = productsService;
            this.customersService = customersService;
        }

        public async Task<(bool IsSuccess, dynamic SearchResults)> SearchAsync(int customerId)
        {
            var ordersResult =  await ordersService.GetOrdersAsync(customerId);
            var productsResult = await productsService.GetProductsAsync();
            var customersResult = await customersService.GetCustomersAsync();

            if (ordersResult.isSuccess)
            {
                foreach (var order in ordersResult.orders)
                {
                    foreach (var item in order.Items)
                    {
                        item.ProductName = productsResult.isSuccess ? productsResult.products.FirstOrDefault(p => p.Id == item.ProductId)?.Name : "Product information is not available";
                    }
                }

                var result = new
                {
                    Orders = ordersResult.orders
                };

                return (true, result);
            }

            return (false, null);
            
        }
    }
}
