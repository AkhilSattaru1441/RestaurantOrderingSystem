using System.ComponentModel.DataAnnotations;

namespace RestaurantOrderingAPI.Models
{
    public class FoodItem
    {
        [Key]
        public int FoodId { get; set; }
        public int CategoryId { get; set; }
        public string? FoodName { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public bool IsVeg { get; set; }
        public string? ImageUrl { get; set; }
        public bool IsAvailable { get; set; } 
    }
}
