using RestaurantOrderingAPI.Models;
namespace RestaurantOrderingAPI.Repositories
{
    public interface IUserRepository
    {
        Task<User?> GetByEmailAsync(string email);

        Task<User?> GetByPhoneNumberAsync(string phoneNumber);
        Task<bool>UpdateAsync(User user);
    }
}
