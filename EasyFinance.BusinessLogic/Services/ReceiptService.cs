using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using EasyFinance.BusinessLogic.Interfaces;
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

        public async Task<IEnumerable<Receipt>> GetReceiptsAsync()
        {
            return await _context.Receipts
                .Include(r=>r.PaymentMethod)
                .Include(r => r.Category)
                .Include(r => r.Currency)
                .ToListAsync();
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
    }
}
