using CurrencyConverter2023.Entities;
using Microsoft.AspNetCore.Identity;
using Org.BouncyCastle.Crypto.Generators;
using System.Security.Cryptography;
using BCrypt.Net;

namespace CurrencyConverter2023.Helpers
{
    public static class PasswordHasher
    {
        public static string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        public static bool VerifyPassword(string password, string hashedPassword)
        {
            return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
        }

    }
}
