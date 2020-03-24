using System.Drawing;
using System.Drawing.Imaging;

namespace EasyFinance.Interfaces
{
    public interface IFileHelper
    {
        byte[] ImageToByteArray(Image image);

        Image ByteArrayToImage(byte[] file);

        ImageFormat GetImageFormatByName(string fileName);
    }
}
