using RestaurantOrderingAPI.DTOs;

namespace RestaurantOrderingAPI.Services
{
    public interface ICategoryService
    {
        Task<CategoryResponseDto> CreateAsync(CategoryCreateDto dto);
        Task<List<CategoryResponseDto>> GetAllAsync();
        Task<CategoryResponseDto?> GetByIdAsync(int id);
        Task<CategoryResponseDto> UpdateAsync(int id, CategoryCreateDto dto);
        Task<bool> DeleteAsync(int id);
    }
}
