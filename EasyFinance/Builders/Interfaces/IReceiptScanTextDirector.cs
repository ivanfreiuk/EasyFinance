using System.Drawing;
using EasyFinance.BusinessLogic.Models;
using EasyFinance.Models;

namespace EasyFinance.Builders.Interfaces
{
    public interface IReceiptScanTextDirector
    {
        ScanText ConstructScanText(Image image, ReceiptTemplate receiptTemplate);
    }
}
