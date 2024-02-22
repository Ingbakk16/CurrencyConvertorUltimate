namespace CurrencyConverter2023.Models.Dtos
{
    public class CurrencyConversionDto
    {
        public int SourceCurrencyId { get; set; }
        public int TargetCurrencyId { get; set; }
        public decimal Amount { get; set; }
    }
}
