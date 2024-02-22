using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CurrencyConverter2023.Entities
{
    public class UserCurrency
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserCurrencyId { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public int CurrencyId { get; set; }
        public Currency Currency { get; set; }
    }
}
