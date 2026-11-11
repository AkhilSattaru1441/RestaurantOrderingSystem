using System.ComponentModel.DataAnnotations;

namespace RestaurantOrderingAPI.DTOs
{
    public class LoginDto
    {
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress ]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

    }
}
