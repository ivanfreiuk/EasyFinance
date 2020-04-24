using System.Collections.Generic;
using System.Threading.Tasks;
using EasyFinance.BusinessLogic.Models;
using EasyFinance.DataAccess.Entities;

namespace EasyFinance.BusinessLogic.Interfaces
{
    public interface IReceiptService
    {
        Task<Receipt> GetReceiptAsync(int id);

        Task<IEnumerable<Receipt>> GetReceiptsAsync(int? userId = null);

        Task<IEnumerable<Receipt>> GetFilteredReceiptsAsync(ReceiptFilterCriteria filterCriteria);

        Task<IEnumerable<Receipt>> GetReceiptsByUserIdAsync(int userId);

        Task<IEnumerable<object>> GetExpensesByCategoriesAsync(int userId);

        Task<IEnumerable<ExpensePeriod>> GetExpensesForPeriodAsync(int userId, bool includeEachDay=false);

        Task AddReceiptAsync(Receipt receipt);

        Task UpdateReceiptAsync(Receipt receipt);

        Task RemoveReceiptAsync(Receipt receipt);
    }
}
