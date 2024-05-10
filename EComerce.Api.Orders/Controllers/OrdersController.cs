using EComerce.Api.Orders.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EComerce.Api.Orders.Controllers
{
    [ApiController]
    [Route("api/orders")]
    public class OrdersController : Controller
    {
        private readonly IOrdersProvider ordersProvider;

        public OrdersController(IOrdersProvider ordersProvider)
        {
            this.ordersProvider = ordersProvider;
        }

        [HttpGet("{customerId}")]
        public async Task<IActionResult> GetOrdersAsync(int customerId)
        {
            var result = await ordersProvider.GetOrdersAsync(customerId);

            if (result.isSuccess) { return Ok(result.orders); }

            return NotFound();
        }
    }
}
