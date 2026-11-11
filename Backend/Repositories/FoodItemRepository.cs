using Microsoft.EntityFrameworkCore;
using RestaurantOrderingAPI.Data;
using RestaurantOrderingAPI.Models;
namespace RestaurantOrderingAPI.Repositories
{
    public class FoodItemRepository : IFoodItemRepository
    {
        private readonly RestaurantDbContext _context;

        public FoodItemRepository(RestaurantDbContext context)
        {
            _context = context;
        }
        public async Task<FoodItem> CreateAsync(FoodItem foodItem)
        {
            _context.FoodItems.Add(foodItem);
            await _context.SaveChangesAsync();

            return foodItem;
        }
        public async Task<List<FoodItem>> GetAllAsync()
        {
            return await _context.FoodItems.ToListAsync();
        }
        public async Task<FoodItem?> GetByIdAsync(int id)
        {
            return await _context.FoodItems.FindAsync(id);
        }
        public async Task<FoodItem> UpdateAsync(FoodItem foodItem)
        {
            _context.FoodItems.Update(foodItem);
            await _context.SaveChangesAsync();

            return foodItem;
        }
        public async Task<bool> DeleteAsync(int id)
        {
            var foodItem = await _context.FoodItems.FindAsync(id);

            if (foodItem == null)
            {
                return false;
            }

            _context.FoodItems.Remove(foodItem);
            await _context.SaveChangesAsync();

            return true;
        }
    }
   
}
