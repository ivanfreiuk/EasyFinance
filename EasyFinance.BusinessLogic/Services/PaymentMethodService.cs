using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EasyFinance.BusinessLogic.Interfaces;
using EasyFinance.DataAccess.Context;
using EasyFinance.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace EasyFinance.BusinessLogic.Services
{
    public class PaymentMethodService: IPaymentMethodService
    {
        private readonly EasyFinanceDbContext _context;

        public PaymentMethodService(EasyFinanceDbContext context)
        {
            _context = context;
        }
        public async Task<PaymentMethod> GetPaymentMethodAsync(int id)
        {
            return await _context.PaymentMethods.FirstOrDefaultAsync(pm => pm.Id == id);
        }

        public async Task<IEnumerable<PaymentMethod>> GetPaymentMethodsAsync()
        {
            return await _context.PaymentMethods.ToListAsync();
        }

        public async Task AddPaymentMethodAsync(PaymentMethod paymentMethod)
        {
            await _context.PaymentMethods.AddAsync(paymentMethod);

            await _context.SaveChangesAsync();
        }

        public async Task UpdatePaymentMethodAsync(PaymentMethod paymentMethod)
        {
            await Task.Run(() => _context.PaymentMethods.Update(paymentMethod));

            await _context.SaveChangesAsync();
        }

        public async Task RemovePaymentMethodAsync(PaymentMethod paymentMethod)
        {
            await Task.Run(() => _context.PaymentMethods.Remove(paymentMethod));

            await _context.SaveChangesAsync();
        }
    }
}
