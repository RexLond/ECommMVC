using ECommMVC.BL.Abstact;
using ECommMVC.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommMVC.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetAll()
        {
            var products = await _productService.GetAllAsync();
            return Ok(products);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Product updatedProduct)
        {
            if (id != updatedProduct.ID)
            {
                return BadRequest("Product ID mismatch");
            }

            var existingProduct = await _productService.GetByIdAsync(id);
            if (existingProduct == null)
            {
                return NotFound("Product not found");
            }

            existingProduct.Name = updatedProduct.Name;

            await _productService.UpdateAsync(existingProduct);
            return NoContent();
        }
    }
}
