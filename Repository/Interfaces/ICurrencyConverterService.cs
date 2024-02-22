using CurrencyConverter2023.Entities;

namespace CurrencyConverter2023.Repository.Interfaces
{
    public interface ICurrencyConverterService
    {
        decimal ConvertCurrency(decimal amount, Currency sourceCurrency, Currency targetCurrency);
    }
}
