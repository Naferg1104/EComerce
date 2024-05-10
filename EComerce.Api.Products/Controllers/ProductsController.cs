using EComerce.Api.Products.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EComerce.Api.Products.Controllers
{
    [ApiController]
    [Route("api/products")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductsProvider _productsProvider;

        public ProductsController(IProductsProvider productsProvider)
        {
            _productsProvider = productsProvider;
        }
        
        public async Task<IActionResult> GetProductsAsync()
        {
            var result = await _productsProvider.GetProductsAsync();

            if(result.isSuccess) { return Ok(result.products); }

            return NotFound();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductAsync(int id)
        {
            var result = await _productsProvider.GetProductAsync(id);

            if (result.isSuccess) { return Ok(result.product); }

            return NotFound();
        }
    }
}
