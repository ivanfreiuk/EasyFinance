using System.Drawing;
using EasyFinance.Builders.Interfaces;
using EasyFinance.BusinessLogic.Models;
using EasyFinance.Interfaces;
using EasyFinance.Models;
using EasyFinance.OCR.Interfaces;

namespace EasyFinance.Builders
{
    public class ReceiptScanTextBuilder: IReceiptScanTextBuilder
    {
        private readonly IReceiptHelper _receiptHelper;
        private readonly IOCRService _ocrService;
        private ScanText _scanText;

        public ReceiptScanTextBuilder(IReceiptHelper receiptHelper, IOCRService ocrService)
        {
            _receiptHelper = receiptHelper;
            _ocrService = ocrService;
            _scanText= new ScanText();
        }

        public IReceiptScanTextBuilder BuildHeaderContent(Image image, BoundingBox boundingBox)
        {
            _scanText.HeaderContent = GetSectionContent(image, boundingBox);
            
            return this;
        }

        public IReceiptScanTextBuilder BuildProductListContent(Image image, BoundingBox boundingBox)
        {
            _scanText.ProductListContent = GetSectionContent(image, boundingBox);

            return this;
        }

        public IReceiptScanTextBuilder BuildFooterContent(Image image, BoundingBox boundingBox)
        {
            _scanText.FooterContent = GetSectionContent(image, boundingBox);

            return this;
        }

        public ScanText GetReceipt()
        {
            var result = _scanText;

            Reset();

            return result;
        }

        public void Reset()
        {
            _scanText = new ScanText(); 
        }

        private string GetSectionContent(Image image, BoundingBox boundingBox)
        {
            var croppedImage = _receiptHelper.CropReceiptSection(image, boundingBox);

            return _ocrService.GetText(croppedImage);
        }
    }
}
