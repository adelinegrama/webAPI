using Shop.DTOs;
using Shop.Data;
using Shop.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace Shop.Services
{
    public class OrderService : IOrderService
    {
        private readonly ShopContext _context;
        private readonly IMapper _mapper;

        public OrderService(ShopContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<OrderDto> CreateAsync(CreateOrderDto dto)
        {
            // load prices
            var productIds = dto.Items.Select(i => i.ProductId).ToList();
            var products = await _context.Products.Where(p => productIds.Contains(p.Id)).ToListAsync();

            var order = _mapper.Map<Order>(dto);
            order.TotalPrice = dto.Items.Sum(i => products.First(p => p.Id == i.ProductId).Price * i.Quantity);

            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
            return _mapper.Map<OrderDto>(order);
        }

        public async Task<IEnumerable<OrderDto>> GetAllAsync()
        {
            var list = await _context.Orders.Include(o => o.Items).ToListAsync();
            return _mapper.Map<IEnumerable<OrderDto>>(list);
        }

        public async Task<OrderDto?> GetByIdAsync(int id)
        {
            var order = await _context.Orders.Include(o => o.Items).FirstOrDefaultAsync(o => o.Id == id);
            return order == null ? null : _mapper.Map<OrderDto>(order);
        }

        public async Task<OrderDto?> UpdateAsync(int id, UpdateOrderDto dto)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order == null) return null;
            _mapper.Map(dto, order);
            await _context.SaveChangesAsync();
            return _mapper.Map<OrderDto>(order);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order == null) return false;
            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}