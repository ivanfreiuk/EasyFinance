using EasyFinance.BusinessLogic.Interfaces;
using System.Collections.Generic;
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

        public async Task RemoveReceiptPhotoAsync(ReceiptPhoto photo)
        {
            await Task.Run(() => _context.ReceiptPhotos.Remove(photo));

            await _context.SaveChangesAsync();
        }

    }
}
