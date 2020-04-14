using System.Drawing;
using System.IO;
using EasyFinance.OCR.Helpers;
using EasyFinance.OCR.Interfaces;
using Tesseract;

namespace EasyFinance.OCR.Services
{
    public class TesseractOCRService: IOCRService
    {
        private readonly TesseractEngine _tesseractEngine;
        private readonly string _tessdataPath;

        public TesseractOCRService()
        {
            _tessdataPath = Path.GetFullPath(@"..\EasyFinance.OCR\Tessdata");
            _tesseractEngine = new TesseractEngine(_tessdataPath, "ukr", EngineMode.Default);
        }

        public string GetText(Image image)
        {
            var buffer = new LocalBuffer();
            buffer.SaveImage(image);
            var extractedText = string.Empty;


            using (var pixImage = Pix.LoadFromFile(buffer.LastSavedFile))
            {
                using (var page = _tesseractEngine.Process(pixImage))
                {
                    extractedText = page.GetText();
                }
            }
            
            buffer.Clear();

            return extractedText;
        }
    }
}
