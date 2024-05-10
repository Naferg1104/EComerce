using AutoMapper;
using EComerce.Api.Costumers.Db;
using EComerce.Api.Costumers.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EComerce.Api.Costumers.Providers
{
    public class CustomersProvider : ICustomersProvider
    {
        private readonly CustomersDbCOntext customersDbCOntext;
        private readonly ILogger<CustomersProvider> logger;
        private readonly IMapper mapper;

        public CustomersProvider(CustomersDbCOntext customersDbCOntext, ILogger<CustomersProvider> logger, IMapper mapper)
        {
            this.customersDbCOntext = customersDbCOntext;
            this.logger = logger;
            this.mapper = mapper;

            SeedData();
        }

        private void SeedData()
        {
            if (!customersDbCOntext.Customers.Any())
            {
                customersDbCOntext.Customers.Add(new Customer() { Id = 1, Name = "Nafer", Address = "Las gaviotas" });
                customersDbCOntext.Customers.Add(new Customer() { Id = 2, Name = "Pedro", Address = "13 de junio" });
                customersDbCOntext.Customers.Add(new Customer() { Id = 3, Name = "Jenifer", Address = "La carolina" });
                customersDbCOntext.Customers.Add(new Customer() { Id = 4, Name = "Isabella", Address = "Centro" });

                customersDbCOntext.SaveChanges();
            }
        }

        public async Task<(bool isSuccess, IEnumerable<Models.Customer> customers, string errorMessage)> GetCustomersAsync()
        {
            try
            {
                var customers = await customersDbCOntext.Customers.ToListAsync();

                if (customers != null && customers.Any())
                {
                    var result = mapper.Map<IEnumerable<Db.Customer>, IEnumerable<Models.Customer>>(customers);

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

        public async Task<(bool isSuccess, Models.Customer customer, string errorMessage)> GetCustomerAsync(int id)
        {
            try
            {
                var customer = await customersDbCOntext.Customers.FirstOrDefaultAsync(p => p.Id == id);

                if (customer != null)
                {
                    var result = mapper.Map<Db.Customer, Models.Customer>(customer);

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
