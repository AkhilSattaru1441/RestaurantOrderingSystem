using System.ComponentModel.DataAnnotations;

namespace RestaurantOrderingAPI.DTOs
{
    public class FoodItemCreateDto
    {
        [Range(1, int.MaxValue)]
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "Food name is required")]
        [StringLength(100, MinimumLength = 2)]
        public string? FoodName { get; set; }

        [StringLength(200)]
        public string? Description { get; set; }

        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than 0")]
        public decimal Price { get; set; }
        public bool IsVeg { get; set; }
        public string? ImageUrl { get; set; }
        public bool IsAvailable { get; set; }

    }
}
