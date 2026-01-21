using AutoMapper;
using Entity_Framework.Data;
using Entity_Framework_Using_DataBase_approach.Models;
using Entity_Framework_Using_DataBase_approach.Models.ViewModels;
using Entity_Framework_Using_DataBase_approach.Services.InterFaces;
using Microsoft.EntityFrameworkCore;

namespace Entity_Framework_Using_DataBase_approach.Services
{

    public class CategoryService : ICategoryService
    {
        private readonly MyDbContext _dbContext;
        private readonly IMapper _mapper;

        public CategoryService(MyDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        // Get all categories
        public async Task<List<CategoryDTO>> GetAllCategoriesAsync()
        {
            var categories = await _dbContext.Categories.ToListAsync();
            return _mapper.Map<List<CategoryDTO>>(categories);
        }

        // Get single category
        public async Task<CategoryDTO?> GetCategoryByIdAsync(int id)
        {
            var category = await _dbContext.Categories.FindAsync(id);
            return category == null ? null : _mapper.Map<CategoryDTO>(category);
        }

        // Check if category exists (optionally exclude current Id for edit)
        public async Task<bool> CategoryExistsAsync(string categoryName, int? idToExclude = null)
        {
            return await _dbContext.Categories
                .AnyAsync(c => c.CategoryName == categoryName && (!idToExclude.HasValue || c.CategoryId != idToExclude.Value));
        }

        // Create a new category
        public async Task<CategoryDTO> CreateCategoryAsync(CategoryDTO dto)
        {
            var category = _mapper.Map<Category>(dto);
            _dbContext.Categories.Add(category);
            await _dbContext.SaveChangesAsync();

            return _mapper.Map<CategoryDTO>(category);
        }

        // Update existing category
        public async Task<bool> UpdateCategoryAsync(CategoryDTO dto)
        {
            var category = await _dbContext.Categories.FindAsync(dto.CategoryId);
            if (category == null)
                return false;

            _mapper.Map(dto, category);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        // Delete category
        public async Task<bool> DeleteCategoryAsync(int id)
        {
            var category = await _dbContext.Categories.FindAsync(id);
            if (category == null)
                return false;

            _dbContext.Categories.Remove(category);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        // Total categories
        public async Task<int> GetTotalCategoriesAsync()
        {
            return await _dbContext.Categories.CountAsync();
        }
    }
}
