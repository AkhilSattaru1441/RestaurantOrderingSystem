using RestaurantOrderingAPI.DTOs;
using RestaurantOrderingAPI.DTOs.User;

namespace RestaurantOrderingAPI.Services
{
    public interface IOrderItemService
    {
        Task<OrderItemResponseDto> CreateAsync(OrderItemCreateDto dto);

        Task<List<OrderItemResponseDto>> GetAllAsync();

        Task<OrderItemResponseDto?> GetByIdAsync(int id, int userId);

        Task<OrderItemResponseDto?> UpdateAsync(int id, OrderItemCreateDto dto);

        Task<bool> DeleteAsync(int id);
    }
}
