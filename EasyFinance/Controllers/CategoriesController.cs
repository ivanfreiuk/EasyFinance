using System.Threading.Tasks;
using EasyFinance.BusinessLogic.Interfaces;
using EasyFinance.DataAccess.Entities;
using Microsoft.AspNetCore.Mvc;

namespace EasyFinance.Controllers
{
    [Route("api/categories")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategoryAsync(int id)
        {
            var category = await _categoryService.GetCategoryAsync(id);

            if (category == null)
            {
                return NotFound();
            }

            return Ok(category);
        }

        [HttpGet]
        public async Task<IActionResult> GetCategoriesAsync()
        {
            var categories = await _categoryService.GetCategoriesAsync();

            if (categories == null)
            {
                return NoContent();
            }

            return Ok(categories);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategoryAsync(Category category)
        {
            await _categoryService.AddCategoryAsync(category);
            
            return Ok(category.Id);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategoryAsync(int id, Category category)
        {
            var categoryFromDb = await _categoryService.GetCategoryAsync(id);

            if (categoryFromDb==null)
            {
                return BadRequest();
            }

            await _categoryService.UpdateCategoryAsync(category);

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategoryAsync(int id)
        {
            var categoryFromDb = await _categoryService.GetCategoryAsync(id);

            if (categoryFromDb == null)
            {
                return BadRequest();
            }

            await _categoryService.RemoveCategoryAsync(categoryFromDb);

            return Ok();
        }


    }
}