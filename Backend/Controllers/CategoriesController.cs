using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestaurantOrderingAPI.DTOs;
using RestaurantOrderingAPI.Services;

namespace RestaurantOrderingAPI.Controllers
{
   
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(CategoryCreateDto   dto)
        {
            var category = await _categoryService.CreateAsync(dto);
            return CreatedAtAction(
                nameof(GetById),
                new { id = category.CategoryId },
                category);
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var categories = await _categoryService.GetAllAsync();
            return Ok(categories);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var category = await _categoryService.GetByIdAsync(id);

            if (category == null)
            {
                return NotFound("category not found");
            }

            return Ok(category);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]    
        public async Task<IActionResult> Update(int id, CategoryCreateDto dto)
        {
            var category = await _categoryService.UpdateAsync(id, dto);

            if (category == null)
            {
                return NotFound("category not found");
            }

            return Ok(category);
        }
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _categoryService.DeleteAsync(id);

            if (!result)
            {
                return NotFound("category not found");
            }

            return Ok("category deleted successfully");
        }
        
    }
}
