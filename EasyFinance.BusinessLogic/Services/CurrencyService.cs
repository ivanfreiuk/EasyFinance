using System.Collections.Generic;
using System.Threading.Tasks;
using EasyFinance.BusinessLogic.Interfaces;
using EasyFinance.DataAccess.Context;
using EasyFinance.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace EasyFinance.BusinessLogic.Services
{
    public class CurrencyService: ICurrencyService
    {
        private readonly EasyFinanceDbContext _context;

        public CurrencyService(EasyFinanceDbContext context)
        {
            _context = context;
        }
        
        public async Task<Currency> GetCurrencyAsync(int id)
        {
            return await _context.Currencies.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<IEnumerable<Currency>> GetCurrenciesAsync()
        {
            return await _context.Currencies.ToListAsync(); 
        }

        public async Task AddCurrencyAsync(Currency currency)
        {
            await _context.Currencies.AddAsync(currency);

            await _context.SaveChangesAsync();
        }

        public async Task UpdateCurrencyAsync(Currency currency)
        {
            await Task.Run(() => _context.Currencies.Update(currency));

            await _context.SaveChangesAsync();
        }

        public async Task RemoveCurrencyAsync(Currency currency)
        {
            await Task.Run(() => _context.Currencies.Remove(currency));

            await _context.SaveChangesAsync();
        }
    }
}
