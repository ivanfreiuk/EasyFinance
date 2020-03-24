using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using EasyFinance.Interfaces;

namespace EasyFinance.Helpers
{
    public class FileHelper: IFileHelper
    {
        public byte[] ImageToByteArray(Image image)
        {
            var stream = new MemoryStream();
            image.Save(stream, image.RawFormat);
            return stream.ToArray();
        }

        public Image ByteArrayToImage(byte[] file)
        {
            var stream = new MemoryStream(file);

            return Image.FromStream(stream);
        }

        public ImageFormat GetImageFormatByName(string fileName)
        {
            throw new System.NotImplementedException();
        }
    }
}
