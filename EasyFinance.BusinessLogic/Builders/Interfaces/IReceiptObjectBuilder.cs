using EasyFinance.DataAccess.Entities;

namespace EasyFinance.BusinessLogic.Builders.Interfaces
{
    public interface IReceiptObjectBuilder
    {
        IReceiptObjectBuilder BuildPaymentMethod(string text);
        IReceiptObjectBuilder BuildCurrency(string text);
        IReceiptObjectBuilder BuildTotalAmount(string text);
        IReceiptObjectBuilder BuildPurchaseDate(string text);
        Receipt GetReceipt();
        void Reset();
    }
}
