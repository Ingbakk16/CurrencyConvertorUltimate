using CurrencyConverter2023.Entities;

namespace CurrencyConverter2023.Repository.Interfaces
{
    public interface ICurrencyService
    {
        List<Currency> GetCurrencies();
        Currency GetCurrency(int id);

        bool IsCurrencyNameTaken(string currencyName);

        bool IsMemoTaken(string memo);
    }
}
