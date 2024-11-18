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
            try
            {
                if (product == null)
                {
                    return BadRequest("Product data is invalid.");
                }

                await _elasticsearchService.IndexProduct(product);
                return Ok(product);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("getProduct")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var product = await _elasticsearchService.GetProduct(id);
                return product != null ? Ok(product) : NotFound($"Product with ID {id} not found.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete("deleteProduct")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var product = await _elasticsearchService.GetProduct(id);
                if (product == null)
                {
                    return NotFound($"Product with ID {id} not found.");
                }

                await _elasticsearchService.DeleteProduct(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("searchProduct")]
        public async Task<IActionResult> Search([FromQuery] string query)
        {
            try
            {
                if (string.IsNullOrEmpty(query))
                {
                    return BadRequest("Search query cannot be empty.");
                }

                var products = await _elasticsearchService.SearchProducts(query);
                return Ok(products);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut("updateProduct/{id}")]
        public async Task<IActionResult> UpdateProduct(int id, [FromBody] Product updatedProduct)
        {
            if (updatedProduct == null || updatedProduct.Id != id)
            {
                return BadRequest("Product data is invalid.");
            }

            try
            {
                var existingProduct = await _elasticsearchService.GetProduct(id);
                if (existingProduct == null)
                {
                    return NotFound($"Product with ID {id} not found.");
                }

                await _elasticsearchService.UpdateProduct(updatedProduct);

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
