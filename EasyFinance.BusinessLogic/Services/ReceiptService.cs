using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using EasyFinance.BusinessLogic.Interfaces;
using EasyFinance.BusinessLogic.Models;
using EasyFinance.DataAccess.Context;
using EasyFinance.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace EasyFinance.BusinessLogic.Services
{
   public class ReceiptService: IReceiptService
    {
        private readonly EasyFinanceDbContext _context;

        public ReceiptService(EasyFinanceDbContext context)
        {
            _context = context;
        }
        public async Task<Receipt> GetReceiptAsync(int id)
        {
            return await _context.Receipts
                .Include(r=>r.ReceiptPhoto)
                .Include(r => r.PaymentMethod)
                .Include(r => r.Category)
                .Include(r => r.Currency)
                .FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<IEnumerable<Receipt>> GetReceiptsAsync(int? userId=null)
        {
            return await _context.Receipts
                .Where(r=> !userId.HasValue || r.UserId==userId)
                .Include(r=>r.ReceiptPhoto)
                .Include(r=>r.PaymentMethod)
                .Include(r => r.Category)
                .Include(r => r.Currency)
                .ToListAsync();
        }

        public async Task<IEnumerable<Receipt>> GetReceiptsByUserIdAsync(int userId)
        {
            return await _context.Receipts
                .Where(r=>r.UserId == userId)
                .Include(r => r.ReceiptPhoto)
                .Include(r => r.PaymentMethod)
                .Include(r => r.Category)
                .Include(r => r.Currency)
                .ToListAsync();
        }

        public async Task<IEnumerable<object>> GetExpensesByCategoriesAsync(int userId)
        {
            return await _context.Receipts
                .Where(r => r.UserId == userId)
                .Include(r => r.Category)
                .GroupBy(r => new
                    {
                        r.CategoryId,
                        CategoryName = r.CategoryId != null ? r.Category.Name : null
                    }
                )
                .Select(group => new
                {
                    group.Key.CategoryId,
                    group.Key.CategoryName,
                    Total = group.Sum(r => r.TotalAmount)
                })
                .ToListAsync();
        }

        public async Task<IEnumerable<ExpensePeriod>> GetExpensesForPeriodAsync(int userId, bool includeEachDay=false)
        {
            var expenses = await _context.Receipts
                .Where(r => r.UserId == userId)
                .GroupBy(r => new
                    {
                        r.UserId,
                        PurchaseDate = r.PurchaseDate.Value.Date
                    }
                )
                .Select(group => new ExpensePeriod
                {
                    UserId = group.Key.UserId,
                    PurchaseDate = group.Key.PurchaseDate,
                    Total = group.Sum(r => r.TotalAmount).Value
                })
                .OrderBy(i => i.PurchaseDate)
                .ToListAsync();

            return includeEachDay ? CreateWholePeriodExpenses(expenses) : expenses;
        }

        public async Task AddReceiptAsync(Receipt receipt)
        {
            await _context.Receipts.AddAsync(receipt);

            await _context.SaveChangesAsync();
        }

        public async Task UpdateReceiptAsync(Receipt receipt)
        {
            await Task.Run(() => _context.Receipts.Update(receipt));

            await _context.SaveChangesAsync();
        }

        public async Task RemoveReceiptAsync(Receipt receipt)
        {
            await Task.Run(() => _context.Receipts.Remove(receipt));

            await _context.SaveChangesAsync();
        }

        private IEnumerable<ExpensePeriod> CreateWholePeriodExpenses(List<ExpensePeriod> expenses)
        {
            var allPeriodExpenses = new List<ExpensePeriod>();

            if (!expenses.Any())
            {
                return allPeriodExpenses;
            }

            var firstDate = expenses.Min(e => e.PurchaseDate).Date;
            var lastDate = expenses.Max(e=>e.PurchaseDate).Date;
            for (var date = firstDate.Date;date<=lastDate.Date; date = date.AddDays(1))
            {
                var expense = expenses.FirstOrDefault(e => e.PurchaseDate.Date == date);

                allPeriodExpenses.Add(expense ?? new ExpensePeriod{ PurchaseDate = date, Total = 0});
            }
            
            return allPeriodExpenses;
        }
    }
}
