using System.ComponentModel.DataAnnotations;

namespace CurrencyConverter2023.Models.Dtos
{
    public class UserForRegistrationDto
    {
        [Required]
        [MaxLength(16)]
        public string UserName { get; set; }

        [Required]
        [MaxLength(225)]
        public string Email { get; set; }

        [Required]
        [MaxLength(225)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(225)]
        public string LastName { get; set; }

        [Required]
        [MaxLength(225)]
        public string Password { get; set; }

    }
}
