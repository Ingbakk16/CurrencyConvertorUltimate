using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CurrencyConverter2023.Entities
{
    public class Currency
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CurrencyId { get; set; }

        [MaxLength(50)]
        public string CurrencyName { get; set; }

        [MaxLength(5)]
        public string CurrencyMemo { get; set; }

        public string Symbol { get; set; }

        public decimal Value { get; set; }

        public ICollection<UserCurrency> UserCurrencies { get; set; }
    }
}
