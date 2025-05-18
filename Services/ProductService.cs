using Shop.DTOs;
using Shop.Data;
using Shop.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace Shop.Services
{
    public class ProductService : IProductService
    {
        private readonly ShopContext _context;
        private readonly IMapper _mapper;

        public ProductService(ShopContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ProductDto> CreateAsync(CreateProductDto dto)
        {
            var entity = _mapper.Map<Product>(dto);
            _context.Products.Add(entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<ProductDto>(entity);
        }

        public async Task<IEnumerable<ProductDto>> GetAllAsync()
        {
            var list = await _context.Products.ToListAsync();
            return _mapper.Map<IEnumerable<ProductDto>>(list);
        }

        public async Task<ProductDto?> GetByIdAsync(int id)
        {
            var entity = await _context.Products.FindAsync(id);
            return entity == null ? null : _mapper.Map<ProductDto>(entity);
        }

        public async Task<ProductDto?> UpdateAsync(int id, UpdateProductDto dto)
        {
            var entity = await _context.Products.FindAsync(id);
            if (entity == null) return null;
            _mapper.Map(dto, entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<ProductDto>(entity);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.Products.FindAsync(id);
            if (entity == null) return false;
            _context.Products.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
