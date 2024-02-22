using System.ComponentModel.DataAnnotations;

namespace CurrencyConverter2023.Models.Dtos
{
    public class AuthenticationRequestDto
    {
        [Required]
        public string? username { get; set; }
        [Required]
        public string? password { get; set; }
    }
}
