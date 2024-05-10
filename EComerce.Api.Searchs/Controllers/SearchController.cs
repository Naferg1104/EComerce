using EComerce.Api.Searchs.Interfaces;
using EComerce.Api.Searchs.Models;
using Microsoft.AspNetCore.Mvc;

namespace EComerce.Api.Searchs.Controllers
{
    [ApiController]
    [Route("api/search")]
    public class SearchController : ControllerBase
    {
        private readonly ISearchService _searchService;

        public SearchController(ISearchService searchService)
        {
            _searchService = searchService;
        }

        [HttpPost]
        public async Task<IActionResult> SearchAsync(SearchTerm term)
        {
            var result = await _searchService.SearchAsync(term.CustomerId);
            if (result.IsSuccess)
            {
                return Ok(result.SearchResults);
            }
            return BadRequest();
        }
    }
}
