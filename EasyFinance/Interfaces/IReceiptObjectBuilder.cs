using EasyFinance.Models;

namespace EasyFinance.Interfaces
{
    public interface IReceiptObjectBuilder
    {
        IReceiptObjectBuilder BuildPaymentMethod(string text);
        IReceiptObjectBuilder BuildTotalAmount(string text);
        IReceiptObjectBuilder BuildPurchaseDate(string text);
        Receipt GetReceipt();
        void Reset();
    }
}
