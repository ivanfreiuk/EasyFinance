using System.Drawing;
using EasyFinance.BusinessLogic.Models;
using EasyFinance.Models;

namespace EasyFinance.Builders.Interfaces
{
    public interface IReceiptScanTextBuilder
    {
        IReceiptScanTextBuilder BuildHeaderContent(Image image, BoundingBox boundingBox);

        IReceiptScanTextBuilder BuildProductListContent(Image image, BoundingBox boundingBox);

        IReceiptScanTextBuilder BuildFooterContent(Image image, BoundingBox boundingBox);

        ScanText GetReceipt();

        void Reset();
    }
}
