using CurrencyConverter2023.Data;
using CurrencyConverter2023.Entities;
using CurrencyConverter2023.Helpers;
using CurrencyConverter2023.Models.Dtos;
using CurrencyConverter2023.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using BCrypt.Net;
using CurrencyConverter2023.Models.Enums;

namespace CurrencyConverter2023.Services
{
    public class UserService : IUserService
    {

        private readonly ConvertorDbContext _dbContext;
        

        public UserService(ConvertorDbContext dbContext)
        {
            _dbContext = dbContext;
           
        }

      

        public User? ValidateUser(AuthenticationRequestDto authRequestBody)
        {
            // Find the user by username
            var user = _dbContext.Users.FirstOrDefault(p => p.UserName == authRequestBody.username);

            if (user != null)
            {
                // Compare the hashed password using BCrypt library's Verify method
                if (BCrypt.Net.BCrypt.Verify(authRequestBody.password, user.Password))
                {
                    // Passwords match, return the user
                    return user;
                }
            }

            // Either user doesn't exist or password is incorrect
            return null;
        }


        public async Task<bool> IsUsernameTaken(string username)
        {
            return await _dbContext.Users.AnyAsync(u => u.UserName == username);
        }

        public async Task<bool> IsEmailTaken(string email)
        {
            return await _dbContext.Users.AnyAsync(u => u.Email == email);
        }

        public async Task CreateUser(User user)
        {
            _dbContext.Users.Add(user);
            await _dbContext.SaveChangesAsync();
        }



        public User GetUserById(int userId)
        {
            return _dbContext.Users.FirstOrDefault(u => u.Id == userId);
        }


        public async Task<User> GetUserByIdAsync(int userId)
        {
            return await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == userId);
        }




        public void UpgradeToPremium(int userId)
        {
            var user = _dbContext.Users.FirstOrDefault(u => u.Id == userId);

            if (user == null)
            {
                throw new ArgumentException("User not found", nameof(userId));
            }

            if (user.SubscriptionType == SubscriptionType.Free)
            {
                user.SubscriptionType = SubscriptionType.Premium;
                user.RemainingConversions = -1; // Set to -1 for infinite conversions
                _dbContext.SaveChanges(); // Save changes to the database using DbContext
            }
            else
            {
                throw new InvalidOperationException("User is already a premium member");
            }
        }

        public void UpdateUser(User user)
        {
            _dbContext.Users.Update(user);
            _dbContext.SaveChanges();
        }


    }
}
