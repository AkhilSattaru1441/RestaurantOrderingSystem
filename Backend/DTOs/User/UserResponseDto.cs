namespace RestaurantOrderingAPI.DTOs.User
{
    public class UserResponseDto
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public int RoleId { get; set; }
        public DateTime? CreatedAt { get; set; }
    }
}
