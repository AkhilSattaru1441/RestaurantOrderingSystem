using System.ComponentModel.DataAnnotations;

namespace RestaurantOrderingAPI.DTOs.User
{
    public class UserCreateDto
    {
        [Required]
        [StringLength(100, MinimumLength = 2)]
        public string UserName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [Phone]
        public string PhoneNumber { get; set; }

        [Required]
        [MinLength(6)]
        public string Password { get; set; }

        [Range(1, int.MaxValue)]
        public int RoleId { get; set; }
    }
}