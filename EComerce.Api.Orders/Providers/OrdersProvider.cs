using AutoMapper;
using EComerce.Api.Orders.Db;
using EComerce.Api.Orders.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EComerce.Api.Orders.Providers
{
    public class OrdersProvider : IOrdersProvider
    {

        private readonly OrdersDbContext ordersDbContext;
        private readonly ILogger<OrdersProvider> logger;
        private readonly IMapper mapper;

        public OrdersProvider(OrdersDbContext ordersDbContext, ILogger<OrdersProvider> logger, IMapper mapper)
        {
            this.ordersDbContext = ordersDbContext;
            this.logger = logger;
            this.mapper = mapper;

            SeedData();
        }

        private void SeedData()
        {
            if (!ordersDbContext.Orders.Any())
            {
                ordersDbContext.Orders.Add(new Order() { Id = 1, CustomerID = 1, OrderDate = new DateTime(), Total = 100});
                ordersDbContext.OrderItems.Add(new OrderItem() { Id = 1,OrderId = 1, ProductId = 1,Quantity = 10, UnitPrice = 400});

                ordersDbContext.SaveChanges();
            }
        }

        public async Task<(bool isSuccess, IEnumerable<Models.Order> orders, string errorMessage)> GetOrdersAsync(int customerId)
        {
            try
            {
                var orders = await ordersDbContext.Orders.Where( o => o.CustomerID == customerId).ToListAsync();

                if (orders != null && orders.Any())
                {
                    var result = mapper.Map<IEnumerable<Db.Order>, IEnumerable<Models.Order>>(orders);

                    return (true, result, null);
                }

                return (false, null, "Not found");

            }
            catch (Exception ex)
            {
                logger?.LogError(ex.ToString());

                return (false, null, ex.Message);
            }
        }
    }
}
