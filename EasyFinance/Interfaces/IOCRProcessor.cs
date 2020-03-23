using System.Drawing;

namespace EasyFinance.Interfaces
{
    public interface IOCRProcessor
    {
        string GetText(Image image);
    }
}
