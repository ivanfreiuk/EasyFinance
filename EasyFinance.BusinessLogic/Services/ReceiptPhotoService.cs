using EasyFinance.BusinessLogic.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using EasyFinance.DataAccess.Context;
using EasyFinance.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace EasyFinance.BusinessLogic.Services
{
    public class ReceiptPhotoService : IReceiptPhotoService
    {
        private readonly EasyFinanceDbContext _context;

        public ReceiptPhotoService(EasyFinanceDbContext context)
        {
            _context = context;
        }
        
        public async Task<ReceiptPhoto> GetReceiptPhotoAsync(int id)
        {
            var photo = await _context.ReceiptPhotos.FirstOrDefaultAsync(i => i.Id == id);
            
            return photo;
        }

        public async Task<IEnumerable<ReceiptPhoto>> GetReceiptPhotosAsync()
        {
            return await _context.ReceiptPhotos.ToListAsync();
        }

        public async Task AddReceiptPhotoAsync(ReceiptPhoto photo)
        {
            await _context.ReceiptPhotos.AddAsync(photo);

            await _context.SaveChangesAsync();
        }

        public async Task RemoveReceiptPhotoAsync(ReceiptPhoto receiptPhoto)
        {
            await Task.Run(() => _context.ReceiptPhotos.Remove(receiptPhoto));

            await _context.SaveChangesAsync();
        }



        public byte[] ImageToByteArray(Image image)
        {
            var stream = new MemoryStream();
            image.Save(stream, image.RawFormat);
            return stream.ToArray();
        }

        private List<ReceiptPhoto> receiptPhotos = new List<ReceiptPhoto>
        {
            new ReceiptPhoto{ ReceiptId = 1, FileName = @"C:\Users\Ivan_Freiuk\Desktop\DIPLOMA\receipts\DIPLOMA.jpg"},
            new ReceiptPhoto{ ReceiptId = 2, FileName = @"C:\Users\Ivan_Freiuk\Desktop\DIPLOMA\receipts\photo_2020-03-03_13-31-58.jpg"},
            new ReceiptPhoto{ ReceiptId = 3, FileName = @"C:\Users\Ivan_Freiuk\Desktop\DIPLOMA\receipts\photo_2020-03-03_13-31-59 (2).jpg"},
            new ReceiptPhoto{ ReceiptId = 4, FileName = @"C:\Users\Ivan_Freiuk\Desktop\DIPLOMA\receipts\photo_2020-03-03_13-31-59 (3).jpg"},
            new ReceiptPhoto{ ReceiptId = 5, FileName = @"C:\Users\Ivan_Freiuk\Desktop\DIPLOMA\receipts\photo_2020-03-03_13-32-02.jpg"},
            new ReceiptPhoto{ ReceiptId = 6, FileName = @"C:\Users\Ivan_Freiuk\Desktop\DIPLOMA\receipts\photo_2020-03-14_22-16-36.jpg"},

        };

    }
}
