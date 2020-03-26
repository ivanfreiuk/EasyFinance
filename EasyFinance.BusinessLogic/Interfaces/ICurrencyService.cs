using System.Collections.Generic;
using System.Threading.Tasks;
using EasyFinance.DataAccess.Entities;

namespace EasyFinance.BusinessLogic.Interfaces
{
    public interface ICurrencyService
    {
        Task<Currency> GetCurrencyAsync(int id);

        Task<IEnumerable<Currency>> GetCurrenciesAsync();

        Task AddCurrencyAsync(Currency currency);

        Task UpdateCurrencyAsync(Currency currency);

        Task RemoveCurrencyAsync(Currency currency);
    }
}
