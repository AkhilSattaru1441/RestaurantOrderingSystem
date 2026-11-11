using Microsoft.EntityFrameworkCore;
using RestaurantOrderingAPI.Data;
using RestaurantOrderingAPI.Models;

namespace RestaurantOrderingAPI.Repositories
{
    public class OrderItemRepository : IOrderItemRepository
    {
        private readonly RestaurantDbContext _context;

        public OrderItemRepository(RestaurantDbContext context)
        {
            _context = context;
        }

        public async Task<OrderItem> CreateAsync(OrderItem orderItem)
        {
            _context.OrderItems.Add(orderItem);
            await _context.SaveChangesAsync();

            return orderItem;
        }

        public async Task<List<OrderItem>> GetAllAsync()
        {
            return await _context.OrderItems.ToListAsync();
        }

        public async Task<OrderItem?> GetByIdAsync(int id)
        {
            return await _context.OrderItems.FindAsync(id);
        }
        public async Task<List<OrderItem>> GetByOrderIdAsync(int orderId)
        {
            return await _context.OrderItems
                .Where(orderItem => orderItem.OrderId == orderId)
                .ToListAsync();
        }

        public async Task<OrderItem> UpdateAsync(OrderItem orderItem)
        {
            _context.OrderItems.Update(orderItem);
            await _context.SaveChangesAsync();

            return orderItem;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var orderItem = await _context.OrderItems.FindAsync(id);

            if (orderItem == null)
            {
                return false;
            }

            _context.OrderItems.Remove(orderItem);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw;
            }
            return true;
        }
    }
}
