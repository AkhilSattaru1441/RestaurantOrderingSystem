using Microsoft.Identity.Client;
using RestaurantOrderingAPI.DTOs;
using RestaurantOrderingAPI.Models;
using RestaurantOrderingAPI.Repositories;
namespace RestaurantOrderingAPI.Services
{
    public class FoodItemService : IFoodItemService

    {
        private readonly IFoodItemRepository _foodItemRepository;
        public FoodItemService(IFoodItemRepository foodItemRepository)
        {
            _foodItemRepository = foodItemRepository;
        }

        public async Task<FoodItemResponseDto> CreateAsync(FoodItemCreateDto dto)
        {
            var foodItem = new FoodItem
            {
                CategoryId = dto.CategoryId,
                FoodName = dto.FoodName,
                Description = dto.Description,
                Price = dto.Price,
                IsVeg = dto.IsVeg,
                ImageUrl = dto.ImageUrl,
                IsAvailable = dto.IsAvailable
            };

            var createdFoodItem = await _foodItemRepository.CreateAsync(foodItem);

            return new FoodItemResponseDto
            {
                FoodId = createdFoodItem.FoodId,
                CategoryId = createdFoodItem.CategoryId,
                FoodName = createdFoodItem.FoodName,
                Description = createdFoodItem.Description,
                Price = createdFoodItem.Price,
                IsVeg = createdFoodItem.IsVeg,
                ImageUrl = createdFoodItem.ImageUrl,
                IsAvailable = createdFoodItem.IsAvailable
            };
        }
        public async Task<List<FoodItemResponseDto>> GetAllAsync()
        {
            var foodItems = await _foodItemRepository.GetAllAsync();

            return foodItems.Select(f => new FoodItemResponseDto
            {
                FoodId = f.FoodId,
                CategoryId = f.CategoryId,
                FoodName = f.FoodName,
                Description = f.Description,
                Price = f.Price,
                IsVeg = f.IsVeg,
                ImageUrl = f.ImageUrl,
                IsAvailable = f.IsAvailable
            }).ToList();
        }
        public async Task<FoodItemResponseDto?> GetByIdAsync(int id)
        {
            var foodItem = await _foodItemRepository.GetByIdAsync(id);

            if (foodItem == null)
            {
                return null;
            }

            return new FoodItemResponseDto
            {
                FoodId = foodItem.FoodId,
                CategoryId = foodItem.CategoryId,
                FoodName = foodItem.FoodName,
                Description = foodItem.Description,
                Price = foodItem.Price,
                IsVeg = foodItem.IsVeg,
                ImageUrl = foodItem.ImageUrl,
                IsAvailable = foodItem.IsAvailable
            };
        }
        public async Task<FoodItemResponseDto?> UpdateAsync(int id, FoodItemCreateDto dto)
        {
            var foodItem = await _foodItemRepository.GetByIdAsync(id);
            if (foodItem == null)
            {
                return null;
            }
            foodItem.CategoryId = dto.CategoryId;
            foodItem.FoodName = dto.FoodName;
            foodItem.Description = dto.Description;
            foodItem.Price = dto.Price;
            foodItem.IsVeg = dto.IsVeg;
            foodItem.ImageUrl = dto.ImageUrl;
            foodItem.IsAvailable = dto.IsAvailable;

            var updatedFoodItem = await _foodItemRepository.UpdateAsync(foodItem);

            return new FoodItemResponseDto
            {
                FoodId = updatedFoodItem.FoodId,
                CategoryId = updatedFoodItem.CategoryId,
                FoodName = updatedFoodItem.FoodName,
                Description = updatedFoodItem.Description,
                Price = updatedFoodItem.Price,
                IsVeg = updatedFoodItem.IsVeg,
                ImageUrl = updatedFoodItem.ImageUrl,
                IsAvailable = updatedFoodItem.IsAvailable

            };

        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _foodItemRepository.DeleteAsync(id);
        }

    }
}
















    

