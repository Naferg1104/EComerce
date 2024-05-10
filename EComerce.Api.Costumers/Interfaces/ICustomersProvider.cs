using AutoMapper;
using EComerce.Api.Costumers.Models;

namespace EComerce.Api.Costumers.Interfaces
{
    public interface ICustomersProvider
    {
        public Task<(bool isSuccess, IEnumerable<Customer> customers, string errorMessage)> GetCustomersAsync();
        public Task<(bool isSuccess, Customer customer, string errorMessage)> GetCustomerAsync(int id);

    }
}
