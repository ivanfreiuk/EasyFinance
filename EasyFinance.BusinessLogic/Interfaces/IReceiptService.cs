using System.Collections.Generic;
using System.Threading.Tasks;
using EasyFinance.DataAccess.Entities;

namespace EasyFinance.BusinessLogic.Interfaces
{
    public interface IReceiptService
    {
        Task<Receipt> GetReceiptAsync(int id);

        Task<IEnumerable<Receipt>> GetReceiptsAsync();

        Task<IEnumerable<object>> GetExpensesByCategoriesAsync();

        Task<IEnumerable<object>> GetExpensesByAllPeriodAsync();

        Task AddReceiptAsync(Receipt receipt);

        Task UpdateReceiptAsync(Receipt receipt);

        Task RemoveReceiptAsync(Receipt receipt);
    }
}
