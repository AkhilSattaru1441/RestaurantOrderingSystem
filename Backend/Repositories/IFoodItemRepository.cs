using RestaurantOrderingAPI.Models;

namespace RestaurantOrderingAPI.Repositories
{
    public interface IFoodItemRepository 
    {
        Task <FoodItem> CreateAsync(FoodItem foodItem);
        Task<List<FoodItem>> GetAllAsync();
        Task<FoodItem?> GetByIdAsync(int id);

        Task<FoodItem> UpdateAsync(FoodItem foodItem);
        Task <bool> DeleteAsync(int id);
    }
}
