using System.Drawing;
using EasyFinance.Interfaces;
using Tesseract;

namespace EasyFinance.Helpers
{
    public class TesseractOCRProcessor: IOCRProcessor
    {
        private readonly TesseractEngine _tesseractEngine;
        private readonly string _tessdataPath = @"C:\Users\Ivan_Freiuk\Desktop\DIPLOMA\tessdata";

        public TesseractOCRProcessor()
        {
            _tesseractEngine = new TesseractEngine(_tessdataPath, "ukr", EngineMode.Default);
        }
        
        public string GetText(Image image)
        {
            var buffer = new LocalBuffer();
            buffer.SaveImage(image);
            var extractedText= string.Empty;

            using (_tesseractEngine)
            {
                using (var pixImage = Pix.LoadFromFile(buffer.LastSavedFile))
                {
                    using (var page = _tesseractEngine.Process(pixImage))
                    {
                        extractedText = page.GetText();
                    }
                }
            }

            buffer.Clear();

            return extractedText;
        }
    }
}
