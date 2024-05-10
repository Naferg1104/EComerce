using EComerce.Api.Orders.Db;

namespace EComerce.Api.Orders.Interfaces
{
    public interface IOrdersProvider
    {
        public Task<(bool isSuccess, IEnumerable<Models.Order> orders, string errorMessage)> GetOrdersAsync(int customerId);
    }
}
