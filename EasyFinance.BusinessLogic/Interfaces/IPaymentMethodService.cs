using System.Collections.Generic;
using System.Threading.Tasks;
using EasyFinance.DataAccess.Entities;

namespace EasyFinance.BusinessLogic.Interfaces
{
    public interface IPaymentMethodService
    {
        Task<PaymentMethod> GetPaymentMethodAsync(int id);

        Task<IEnumerable<PaymentMethod>> GetPaymentMethodsAsync();

        Task AddPaymentMethodAsync(PaymentMethod paymentMethod);

        Task UpdatePaymentMethodAsync(PaymentMethod paymentMethod);

        Task RemovePaymentMethodAsync(PaymentMethod paymentMethod);
    }
}
