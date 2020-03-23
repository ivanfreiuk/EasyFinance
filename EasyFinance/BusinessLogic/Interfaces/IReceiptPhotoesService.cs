using EasyFinance.BusinessLogic.Models;

namespace EasyFinance.BusinessLogic.Interfaces
{
    public interface IReceiptPhotoService
    {
        ReceiptPhoto GetReceiptPhoto(int receiptId);
    }
}
