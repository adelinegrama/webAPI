using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shop.Data;
using Shop.Models;

namespace Shop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ShopContext _shopContext;

        public ProductController(ShopContext shopContext)
        {
            _shopContext = shopContext;
        }

        // Create
        [HttpPost]
        public async Task<ActionResult<Product>> AddProduct(Product product)
        {
            _shopContext.Products.Add(product);
            await _shopContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetProduct), new { id = product.Id, name = product.Name, 
            brand = product.Brand, price = product.Price}, product);
        }

        // Read
        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            var products = await _shopContext.Products.ToListAsync();
            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var product = await _shopContext.Products.FindAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            return product;
        }

        // Update
        [HttpPut("{id}")]
        public async Task<ActionResult<Product>> UpdateProduct(int id, [FromBody] Product updatedProduct)
        {
            var product = await _shopContext.Products.FindAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            product.Name = updatedProduct.Name;
            product.Brand = updatedProduct.Brand;
            product.Price = updatedProduct.Price;

            await _shopContext.SaveChangesAsync();

            return Ok(product);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await _shopContext.Products.FindAsync(id);

            if (product == null)
            {
                return NotFound("Product not found.");
            }

            _shopContext.Products.Remove(product);
            await _shopContext.SaveChangesAsync();

            return NoContent();
        }
    }
}
