using CurrencyConverter2023.Data;
using CurrencyConverter2023.Entities;
using CurrencyConverter2023.Models.Dtos;
using CurrencyConverter2023.Models.Enums;
using CurrencyConverter2023.Repository.Interfaces;
using CurrencyConverter2023.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CurrencyConverter2023.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CurrencyController : ControllerBase
    {
        private readonly ConvertorDbContext _dbContext;
        private readonly ICurrencyService _currencyService;
        private readonly ICurrencyConverterService _converterService;
        private readonly IUserService _userService;
        private readonly IHttpContextAccessor _httpContextAccessor;



        public CurrencyController(ConvertorDbContext dbContext, ICurrencyService currencyService, ICurrencyConverterService converterService,IUserService userService, IHttpContextAccessor httpContextAccessor)
        {
            _dbContext = dbContext;
            _currencyService = currencyService;
            _converterService = converterService;
            _userService = userService;
            _httpContextAccessor = httpContextAccessor;
        }











        [Authorize(Roles = "Admin")]
        [HttpPost("create")]
        public IActionResult CreateCurrency([FromBody] CurrencyForCreationDto currencyDto)
        {
            if (currencyDto == null)
            {
                return BadRequest("");
            }

            // Check if currency with the same name already exists
            if (_currencyService.IsCurrencyNameTaken(currencyDto.CurrencyName))
            {
                return BadRequest("Currency with the same name already exists.");
            }

            // Check if currency with the same memo already exists
            if (_currencyService.IsMemoTaken(currencyDto.CurrencyMemo))
            {
                return BadRequest("Currency with the same memo already exists.");
            }

            var currency = new Currency
            {
                CurrencyName = currencyDto.CurrencyName,
                CurrencyMemo = currencyDto.CurrencyMemo,
                Symbol = currencyDto.Symbol,
                Value = currencyDto.Value
            };

            _dbContext.Currencies.Add(currency);
            _dbContext.SaveChanges();

            return Ok("Currency successfully created.");
        }

        [Authorize]
        [HttpDelete("delete{id}")]
        public IActionResult DeleteCurrency(int id)
        {
            var currency = _dbContext.Currencies.Find(id);

            if (currency == null)
            {
                return NotFound();
            }

            _dbContext.Currencies.Remove(currency);
            _dbContext.SaveChanges();

            return NoContent();
        }


        [Authorize]
        [HttpPost("convert{userId}")]
        public IActionResult ConvertCurrency(int userId, [FromBody] CurrencyConversionDto conversionDto)
        {
            if (conversionDto == null)
            {
                return BadRequest();
            }

            var sourceCurrency = _currencyService.GetCurrency(conversionDto.SourceCurrencyId);
            var targetCurrency = _currencyService.GetCurrency(conversionDto.TargetCurrencyId);

            if (sourceCurrency == null || targetCurrency == null)
            {
                return NotFound("Source or target currency not found.");
            }



            User user = _userService.GetUserById(userId);

            // Check if the user is null before accessing its properties or methods
            if (user != null)
            {
                if (user.SubscriptionType == SubscriptionType.Free && user.RemainingConversions <= 0)
                {
                    return BadRequest("You have exceeded your free conversion limit.");
                }
                else if (user.SubscriptionType == SubscriptionType.Free)
                {
                    user.RemainingConversions--;
                    _userService.UpdateUser(user);
                }

                // Perform currency conversion
                decimal convertedAmount = _converterService.ConvertCurrency(conversionDto.Amount, sourceCurrency, targetCurrency);

                var result = new
                {
                    SourceCurrency = sourceCurrency.CurrencyName,
                    TargetCurrency = targetCurrency.CurrencyName,
                    Amount = conversionDto.Amount,
                    ConvertedAmount = convertedAmount
                };

                return Ok(result);
            }
            else
            {
                // Handle the case where the user is null
                return BadRequest("User not found or invalid user ID.");
            }
        }




        [HttpGet("currencies")]
        public IActionResult GetCurrencies()
        {
            var currencies = _dbContext.Currencies.ToList();

            return Ok(currencies);
        }


    }
}

