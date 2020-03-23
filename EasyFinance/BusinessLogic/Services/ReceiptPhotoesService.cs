using EasyFinance.BusinessLogic.Interfaces;
using System.Collections.Generic;
using System.Linq;
using EasyFinance.BusinessLogic.Models;
using System.Drawing;
using System.IO;

namespace EasyFinance.BusinessLogic.Services
{
    public class ReceiptPhotoService : IReceiptPhotoService
    {
        private List<ReceiptPhoto> receiptPhotos = new List<ReceiptPhoto>
        {
            new ReceiptPhoto{ ReceiptId = 1, FileName = @"C:\Users\Ivan_Freiuk\Desktop\DIPLOMA\receipts\DIPLOMA.jpg"},
            new ReceiptPhoto{ ReceiptId = 2, FileName = @"C:\Users\Ivan_Freiuk\Desktop\DIPLOMA\receipts\photo_2020-03-03_13-31-58.jpg"},
            new ReceiptPhoto{ ReceiptId = 3, FileName = @"C:\Users\Ivan_Freiuk\Desktop\DIPLOMA\receipts\photo_2020-03-03_13-31-59 (2).jpg"},
            new ReceiptPhoto{ ReceiptId = 4, FileName = @"C:\Users\Ivan_Freiuk\Desktop\DIPLOMA\receipts\photo_2020-03-03_13-31-59 (3).jpg"},
            new ReceiptPhoto{ ReceiptId = 5, FileName = @"C:\Users\Ivan_Freiuk\Desktop\DIPLOMA\receipts\photo_2020-03-03_13-32-02.jpg"},
            new ReceiptPhoto{ ReceiptId = 6, FileName = @"C:\Users\Ivan_Freiuk\Desktop\DIPLOMA\receipts\photo_2020-03-14_22-16-36.jpg"},

        };

        public ReceiptPhoto GetReceiptPhoto(int receiptId)
        {
            var photo = receiptPhotos.FirstOrDefault(i => i.ReceiptId == receiptId);
            var image = Image.FromFile(photo.FileName);
            
            //var stream = new MemoryStream();
            //image.Save(stream, ImageFormat.Png);

            photo.FileBytes = ImageToByteArray(image);

            return photo;
        }

        public byte[] ImageToByteArray(Image image)
        {
            var stream = new MemoryStream();
            image.Save(stream, image.RawFormat);
            return stream.ToArray();
        }

        
    }
}
