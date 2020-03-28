using System.Collections.Generic;
using System.Threading.Tasks;
using EasyFinance.BusinessLogic.Interfaces;
using EasyFinance.DataAccess.Context;
using EasyFinance.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace EasyFinance.BusinessLogic.Services
{
    public class CategoryService: ICategoryService
    {
        private readonly EasyFinanceDbContext _context;

        public CategoryService(EasyFinanceDbContext context)
        {
            _context = context;
        }
        public async Task<Category> GetCategoryAsync(int id)
        {
            return await _context.Categories.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<IEnumerable<Category>> GetCategoriesAsync()
        {
            return await _context.Categories.ToListAsync();
        }

        public async Task AddCategoryAsync(Category category)
        {
            await _context.Categories.AddAsync(category);

            await _context.SaveChangesAsync();
        }

        public async Task UpdateCategoryAsync(Category category)
        {
            await Task.Run(() => _context.Categories.Update(category));

            await _context.SaveChangesAsync();
        }

        public async Task RemoveCategoryAsync(Category category)
        {
            await Task.Run(() => _context.Categories.Remove(category));

            await _context.SaveChangesAsync();
        }
    }
}
