namespace EComerce.Api.Searchs.Interfaces
{
    public interface IOrdersService
    {
        public Task<(bool isSuccess, IEnumerable<Models.Order> orders, string errorMessage)> GetOrdersAsync(int customerId);
    }
}
