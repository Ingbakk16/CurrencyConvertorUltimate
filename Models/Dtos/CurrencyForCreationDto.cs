namespace CurrencyConverter2023.Models.Dtos
{
    public class CurrencyForCreationDto
    {
        public string CurrencyName { get; set; }
        public string CurrencyMemo { get; set; }
        public string Symbol { get; set; }
        public decimal Value { get; set; }
    }
}
