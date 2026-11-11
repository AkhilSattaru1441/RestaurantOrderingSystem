
using RestaurantOrderingAPI.DTOs;
using RestaurantOrderingAPI.Models;
using RestaurantOrderingAPI.Repositories;
using System.Runtime;

namespace RestaurantOrderingAPI.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<CategoryResponseDto> CreateAsync(CategoryCreateDto dto)
        {
            var category = new Category
            {
                CategoryName = dto.CategoryName,
                Description = dto.Description,
                IsActive = dto.IsActive
            };

            var createdCategory = await _categoryRepository.CreateAsync(category);

            return new CategoryResponseDto
            {
                CategoryId = createdCategory.CategoryId,
                CategoryName = createdCategory.CategoryName,
                Description = createdCategory.Description,
                IsActive = createdCategory.IsActive
            };
        }
        public async Task<List<CategoryResponseDto>> GetAllAsync()
        {
            var categories = await _categoryRepository.GetAllAsync();

            return categories.Select(c => new CategoryResponseDto
            {
                CategoryId = c.CategoryId,
                CategoryName = c.CategoryName,
                Description = c.Description,
                IsActive = c.IsActive
            }).ToList();
        }

        public async Task<CategoryResponseDto?> GetByIdAsync(int id)
        {
            var category = await _categoryRepository.GetByIdAsync(id);

            if (category == null)
            {
                return null;
            }

            return new CategoryResponseDto
            {
                CategoryId = category.CategoryId,
                CategoryName = category.CategoryName,
                Description = category.Description,
                IsActive = category.IsActive
            };
        }
        public async Task<CategoryResponseDto> UpdateAsync(int id, CategoryCreateDto dto)
        {
            var category = await _categoryRepository.GetByIdAsync(id);

            if (category == null)
            {
                return null;
            }

            category.CategoryName = dto.CategoryName;
            category.Description = dto.Description;
            category.IsActive = dto.IsActive;

            var updatedCategory = await _categoryRepository.UpdateAsync(category);

            return new CategoryResponseDto
            {
                CategoryId = updatedCategory.CategoryId,
                CategoryName = updatedCategory.CategoryName,
                Description = updatedCategory.Description,
                IsActive = updatedCategory.IsActive
            };
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _categoryRepository.DeleteAsync(id);
        }
    }
   
}