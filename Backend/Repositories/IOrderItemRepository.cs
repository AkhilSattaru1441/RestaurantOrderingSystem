using RestaurantOrderingAPI.Models;

namespace RestaurantOrderingAPI.Repositories
{
    public interface IOrderItemRepository
    {
        Task<OrderItem> CreateAsync(OrderItem orderItem);

        Task<List<OrderItem>> GetAllAsync();

        Task<OrderItem?> GetByIdAsync(int id);
        Task<List<OrderItem>> GetByOrderIdAsync(int orderId);
        Task<OrderItem> UpdateAsync(OrderItem orderItem);

        Task<bool> DeleteAsync(int id);
    }
}