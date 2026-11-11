using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using RestaurantOrderingAPI.Services;
using RestaurantOrderingAPI.DTOs;
using Microsoft.AspNetCore.Authorization;

namespace RestaurantOrderingAPI.Controllers
{ 
    [ApiController]
    [Route("api/[controller]")]
    public class FoodItemsController : ControllerBase
    {
        private readonly IFoodItemService _foodItemService;

        public FoodItemsController(IFoodItemService foodItemService)
        {
            _foodItemService = foodItemService;
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> Create(FoodItemCreateDto dto)
        {
            var foodItem = await _foodItemService.CreateAsync(dto);
            return CreatedAtAction(
                nameof(GetById),
                new { id = foodItem.FoodId },
                foodItem);
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var foodItems = await _foodItemService.GetAllAsync();
            return Ok(foodItems);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var foodItem = await _foodItemService.GetByIdAsync(id);

            if (foodItem == null)
            {
                return NotFound("Food item not found");
            }

            return Ok(foodItem);
        }
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> Update(int id, FoodItemCreateDto dto)
        {
            var foodItem = await _foodItemService.UpdateAsync(id, dto);

            if (foodItem == null)
            {
                return NotFound();
            }

            return Ok(foodItem);
        }
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> Delete(int id)
        {
            var result = await _foodItemService.DeleteAsync(id);

            if (!result)
            {
                return NotFound();
            }

            return Ok(new { message = "Food item deleted successfully." });
        }
    }
}
