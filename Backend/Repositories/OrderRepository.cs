using Microsoft.EntityFrameworkCore;
using RestaurantOrderingAPI.Data;
using RestaurantOrderingAPI.Models;
using System.Runtime.CompilerServices;

namespace RestaurantOrderingAPI.Repositories
{
    public class OrderRepository : IOrderRepository 
    {
        private readonly RestaurantDbContext _context; 

        public OrderRepository(RestaurantDbContext context)
        {
            _context = context;
        }

        public async Task<Order> CreateAsync(Order order)
        {
            _context.orders.Add(order);
            await _context.SaveChangesAsync();

            return order;
        }

        public async Task<List<Order>> GetAllAsync()
        {
            return await _context.orders.ToListAsync();
        }

        public async Task<Order?> GetByIdAsync(int id)
        {
            return await _context.orders.FindAsync(id);
        }

        public async Task<Order> UpdateAsync(Order order)
        {
            _context.orders.Update(order);
            await _context.SaveChangesAsync();

            return order;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var order = await _context.orders.FindAsync(id);

            if (order == null)
            {
                return false;
            }

            _context.orders.Remove(order);
            await _context.SaveChangesAsync();

            return true;
        }

    }
}
