using RestaurantOrderingAPI.Models;
namespace RestaurantOrderingAPI.Repositories
{
    public interface IOrderRepository
    {
        Task<Order> CreateAsync(Order order);
        Task<List<Order>> GetAllAsync();
        Task<Order?> GetByIdAsync(int id);
        Task<Order> UpdateAsync(Order order);
        Task<bool> DeleteAsync(int id);

    }
}
