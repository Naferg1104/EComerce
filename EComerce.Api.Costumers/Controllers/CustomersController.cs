using EComerce.Api.Costumers.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EComerce.Api.Costumers.Controllers
{
    [ApiController]
    [Route("api/customers")]
    public class CustomersController : Controller
    {
        private readonly ICustomersProvider customersProvider;

        public CustomersController(ICustomersProvider customersProvider)
        {
            this.customersProvider = customersProvider;
        }

        public async Task<IActionResult> GetCustomersAsync()
        {
            var result = await customersProvider.GetCustomersAsync();

            if (result.isSuccess) { return Ok(result.customers); }

            return NotFound();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCustomerAsync(int id)
        {
            var result = await customersProvider.GetCustomerAsync(id);

            if (result.isSuccess) { return Ok(result.customer); }

            return NotFound();
        }
    }
}
