using RestaurantOrderingAPI.DTOs;

namespace RestaurantOrderingAPI.Services
{
    public interface IFoodItemService
    {
        Task<FoodItemResponseDto> CreateAsync(FoodItemCreateDto dto);
        Task<List<FoodItemResponseDto>> GetAllAsync();
        Task<FoodItemResponseDto?> GetByIdAsync(int id);
        Task<FoodItemResponseDto> UpdateAsync(int id, FoodItemCreateDto dto);
        Task<bool> DeleteAsync(int id);

    }
}
