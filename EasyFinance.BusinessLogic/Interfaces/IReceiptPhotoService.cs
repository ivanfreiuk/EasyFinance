using System.Collections.Generic;
using System.Threading.Tasks;
using EasyFinance.DataAccess.Entities;

namespace EasyFinance.BusinessLogic.Interfaces
{
    public interface IReceiptPhotoService
    {
        Task<ReceiptPhoto> GetReceiptPhotoAsync(int id);

        Task<IEnumerable<ReceiptPhoto>> GetReceiptPhotosAsync();

        Task AddReceiptPhotoAsync(ReceiptPhoto photo);

        Task RemoveReceiptPhotoAsync(ReceiptPhoto receiptPhoto);

    }
}
