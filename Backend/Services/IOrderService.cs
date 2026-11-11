using RestaurantOrderingAPI.DTOs;

namespace RestaurantOrderingAPI.Services
{
    public interface IOrderService
    {
        Task<OrderResponseDto>  CreateAsync(OrderCreateDto dto,int userId);
        Task<List<OrderResponseDto>> GetAllAsync();
        Task<OrderResponseDto?> GetByIdAsync(int id, int userId);
        Task<OrderResponseDto> UpdateAsync(int id, OrderCreateDto dto, int userId);
        Task<bool> DeleteAsync(int id,int userId);

    }
}

