using CurrencyConverter2023.Data;
using CurrencyConverter2023.Entities;
using CurrencyConverter2023.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CurrencyConverter2023.Services
{
    public class CurrencyService: ICurrencyService
    {
        private readonly ConvertorDbContext _context;

        public CurrencyService(ConvertorDbContext context)
        {
            _context = context;
        }

        public List<Currency> GetCurrencies()
        {
            return _context.Currencies.ToList();
        }

        public Currency GetCurrency(int id)
        {
            return _context.Currencies.Find(id);
        }


        public bool IsMemoTaken(string memo)
        {
            return _context.Currencies.Any(c => c.CurrencyMemo == memo);
        }

        public bool IsCurrencyNameTaken(string currencyName)
        {
            return _context.Currencies.Any(c => c.CurrencyName == currencyName);
        }
    }
}
