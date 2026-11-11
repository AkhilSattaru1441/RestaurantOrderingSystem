using System.ComponentModel.DataAnnotations;

namespace RestaurantOrderingAPI.DTOs
{
    public class CategoryCreateDto
    {
        [Required]
        [StringLength(100, MinimumLength = 2)]
        public string CategoryName { get; set; }

        [StringLength(200)]
        public string? Description { get; set; }
        public bool IsActive { get; set; }  
    }
}
