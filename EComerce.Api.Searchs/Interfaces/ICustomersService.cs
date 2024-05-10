using EComerce.Api.Searchs.Models;

namespace EComerce.Api.Searchs.Interfaces
{
    public interface ICustomersService
    {
        public Task<(bool isSuccess, IEnumerable<Customer> customers, string errorMessage)> GetCustomersAsync();
    }
}
