using elasticsearch_demo_project.Models;
using elasticsearch_demo_project.Services;
using Microsoft.AspNetCore.Mvc;

namespace elasticsearch_demo_project.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly ElasticsearchService _elasticsearchService;

        public ProductController(ElasticsearchService elasticsearchService)
        {
            _elasticsearchService = elasticsearchService;
        }

        [HttpPost("createProduct")]
        public async Task<IActionResult> Create(Product product)
        {
            await _elasticsearchService.IndexProduct(product);
            return Ok(product);
        }

        [HttpGet("getProduct")]
        public async Task<IActionResult> Get(int id)
        {
            var product = await _elasticsearchService.GetProduct(id);
            return product != null ? Ok(product) : NotFound();
        }

        [HttpDelete("deleteProduct")]
        public async Task<IActionResult> Delete(int id)
        {
            await _elasticsearchService.DeleteProduct(id);
            return NoContent();
        }

        [HttpGet("searchProduct")]
        public async Task<IActionResult> Search([FromQuery] string query)
        {
            var product = await _elasticsearchService.SearchProducts(query);
            return Ok(product);
        }
    }
}
