using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shop.Data;
using Shop.Models;
using Shop.DTOs;
using AutoMapper;

namespace Shop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ShopContext _shopContext;
        private readonly IMapper _mapper;

        public ProductController(ShopContext shopContext, IMapper mapper)
        {
            _shopContext = shopContext;
            _mapper = mapper;
        }

        // Create
        [HttpPost]
        public async Task<ActionResult<ProductDto>> AddProduct([FromBody] CreateProductDto createDto)
        {
            var product = _mapper.Map<Product>(createDto);
            _shopContext.Products.Add(product);
            await _shopContext.SaveChangesAsync();

            var productDto = _mapper.Map<ProductDto>(product);
            return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, productDto);
        }

        // Read all
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetProducts()
        {
            var products = await _shopContext.Products.ToListAsync();
            return Ok(_mapper.Map<IEnumerable<ProductDto>>(products));
        }

        // Read by id
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDto>> GetProduct(int id)
        {
            var product = await _shopContext.Products.FindAsync(id);
            if (product == null)
                return NotFound();

            return _mapper.Map<ProductDto>(product);
        }

        // Update
        [HttpPut("{id}")]
        public async Task<ActionResult<ProductDto>> UpdateProduct(int id, [FromBody] UpdateProductDto updateDto)
        {
            var product = await _shopContext.Products.FindAsync(id);
            if (product == null)
                return NotFound();

            _mapper.Map(updateDto, product);
            await _shopContext.SaveChangesAsync();

            return Ok(_mapper.Map<ProductDto>(product));
        }

        // Delete
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await _shopContext.Products.FindAsync(id);
            if (product == null)
                return NotFound("Product not found.");

            _shopContext.Products.Remove(product);
            await _shopContext.SaveChangesAsync();

            return NoContent();
        }
    }
}
