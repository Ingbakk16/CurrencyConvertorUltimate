using CurrencyConverter2023.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net.Mail;

namespace CurrencyConverter2023.Entities
{
    public class User
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

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

        public State State { get; set; } = State.Active;

        public Role Role { get; set; } = Role.User;

        public SubscriptionType SubscriptionType { get; set; } = SubscriptionType.Free;
        public int RemainingConversions { get; set; } = 10; 

        public ICollection<UserCurrency> UserCurrencies { get; set; }

      

    }
}
