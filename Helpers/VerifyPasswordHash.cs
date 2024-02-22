using CurrencyConverter2023.Entities;
using Microsoft.AspNetCore.Identity;

namespace CurrencyConverter2023.Helpers
{
    public class VerifyPasswordHash
    {
        public static  bool VerifyPassword(string enteredPassword, string storedHashedPassword)
        {
            if (enteredPassword == null)
                throw new ArgumentNullException(nameof(enteredPassword));

            if (storedHashedPassword == null)
                throw new ArgumentNullException(nameof(storedHashedPassword));

            try
            {
                // Use a proper password hashing library or method
                // Here, we'll use Microsoft.AspNetCore.Identity.PasswordHasher
                var passwordHasher = new PasswordHasher<User>();
                var passwordVerificationResult = passwordHasher.VerifyHashedPassword(null, storedHashedPassword, enteredPassword);

                return passwordVerificationResult == PasswordVerificationResult.Success;
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as appropriate
                Console.WriteLine($"Error verifying password: {ex.Message}");
                return false; // Indicate failure
            }
        }
    }
}
