using EasyFinance.BusinessLogic.Builders.Interfaces;
using EasyFinance.BusinessLogic.Models;
using EasyFinance.DataAccess.Entities;

namespace EasyFinance.BusinessLogic.Builders
{
    public class ReceiptObjectDirector: IReceiptObjectDirector
    {
        private readonly IReceiptObjectBuilder _receiptObjectBuilder;

        public ReceiptObjectDirector(IReceiptObjectBuilder receiptObjectBuilder)
        {
            _receiptObjectBuilder = receiptObjectBuilder;
        }
        public Receipt ConstructReceipt(ScanText scanText)
        {
            var receipt = _receiptObjectBuilder.BuildTotalAmount(scanText.FooterContent)
                .BuildCurrency(scanText.FooterContent)
                .BuildPurchaseDate(scanText.FooterContent)
                .BuildPaymentMethod(scanText.FooterContent)
                .GetReceipt();

            return receipt;
        }
    }
}
