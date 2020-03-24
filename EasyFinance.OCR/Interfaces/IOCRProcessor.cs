using System.Drawing;

namespace EasyFinance.OCR.Interfaces
{
    public interface IOCRService
    {
        string GetText(Image image);
    }
}
