using CurrencyConverter2023.Entities;
using CurrencyConverter2023.Models.Dtos;

namespace CurrencyConverter2023.Repository.Interfaces
{
    public interface IUserService
    {
        User? ValidateUser(AuthenticationRequestDto authRequestBody);

        Task<bool> IsUsernameTaken(string username);

        Task<bool> IsEmailTaken(string email);

        Task CreateUser(User user);

        User GetUserById(int userId);

        Task<User> GetUserByIdAsync(int userId);

        void UpgradeToPremium(int userId);

        public void UpdateUser(User user);
    }
}
