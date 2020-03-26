using System.Drawing;
using EasyFinance.Builders.Interfaces;
using EasyFinance.BusinessLogic.Models;
using EasyFinance.Models;

namespace EasyFinance.Builders
{
    public class ReceiptScanTextDirector:IReceiptScanTextDirector
    {
        private readonly IReceiptScanTextBuilder _scanTextBuilder;

        public ReceiptScanTextDirector(IReceiptScanTextBuilder scanTextBuilder)
        {
            _scanTextBuilder = scanTextBuilder;
        }
        public ScanText ConstructScanText(Image image, ReceiptTemplate receiptTemplate)
        {
           return _scanTextBuilder.BuildHeaderContent(image, receiptTemplate.HeaderSection.BoundingBox)
                .BuildProductListContent(image, receiptTemplate.ProductListSection.BoundingBox)
                .BuildFooterContent(image, receiptTemplate.FooterSection.BoundingBox)
                .GetReceipt();
        }
    }
}
