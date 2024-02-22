using CurrencyConverter2023.Entities;
using CurrencyConverter2023.Repository.Interfaces;

namespace CurrencyConverter2023.Services
{
    public class CurrencyConverterService : ICurrencyConverterService
    {
        public decimal ConvertCurrency(decimal amount, Currency sourceCurrency, Currency targetCurrency)
        {
            if (sourceCurrency == null || targetCurrency == null)
            {
                throw new ArgumentException("Source and target currencies must be provided.");
            }

            if (sourceCurrency.Value == 0 || targetCurrency.Value == 0)
            {
                throw new InvalidOperationException("Invalid currency value for conversion.");
            }

            // Conversion formula: amount * (targetCurrency.Value / sourceCurrency.Value)
            decimal convertedAmount = amount * (targetCurrency.Value / sourceCurrency.Value);

            return convertedAmount;
        }
    }
}
