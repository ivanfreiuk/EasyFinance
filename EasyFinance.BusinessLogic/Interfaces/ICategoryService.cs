using System.Collections.Generic;
using System.Threading.Tasks;
using EasyFinance.DataAccess.Entities;

namespace EasyFinance.BusinessLogic.Interfaces
{
    public interface ICategoryService
    {
        Task<Category> GetCategoryAsync(int id);

        Task<IEnumerable<Category>> GetCategoriesAsync();

        Task AddCategoryAsync(Category category);

        Task UpdateCategoryAsync(Category category);

        Task RemoveCategoryAsync(Category category);
    }
}
