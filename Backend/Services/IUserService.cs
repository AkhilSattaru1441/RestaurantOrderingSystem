using RestaurantOrderingAPI.DTOs;

namespace RestaurantOrderingAPI.Services
{
    public interface IUserService
    {
        Task<string> LoginAsync(LoginDto loginDto);
        Task<bool> ChangePasswordAsync(ChangePasswordDto dto);
        Task<bool> ForgotPasswordAsync(ForgotPasswordDto dto);

    }
}
