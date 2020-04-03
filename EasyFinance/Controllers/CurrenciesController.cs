using System.Threading.Tasks;
using EasyFinance.BusinessLogic.Interfaces;
using EasyFinance.DataAccess.Entities;
using Microsoft.AspNetCore.Mvc;

namespace EasyFinance.Controllers
{
    [Route("api/currencies")]
    [ApiController]
    public class CurrenciesController : ControllerBase
    {
        private readonly ICurrencyService _currencyService;

        public CurrenciesController(ICurrencyService currencyService)
        {
            _currencyService = currencyService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCurrencyAsync(int id)
        {
            var currency = await _currencyService.GetCurrencyAsync(id);

            if (currency == null)
            {
                return NotFound();
            }

            return Ok(currency);
        }

        [HttpGet]
        public async Task<IActionResult> GetCurrenciesAsync()
        {
            var currencies = await _currencyService.GetCurrenciesAsync();

            if (currencies == null)
            {
                return NoContent();
            }

            return Ok(currencies);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCurrencyAsync(Currency currency)
        {
            await _currencyService.AddCurrencyAsync(currency);

            return Ok(currency.Id);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCurrencyAsync(int id, Currency currency)
        {
            var currencyFromDb = await _currencyService.GetCurrencyAsync(id);

            if (currencyFromDb == null)
            {
                return BadRequest();
            }

            await _currencyService.UpdateCurrencyAsync(currencyFromDb);

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategoryAsync(int id)
        {
            var currencyFromDb = await _currencyService.GetCurrencyAsync(id);

            if (currencyFromDb == null)
            {
                return BadRequest();
            }

            await _currencyService.RemoveCurrencyAsync( currencyFromDb);

            return Ok();
        }
    }
}