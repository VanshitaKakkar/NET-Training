using Entity_Framework_Using_DataBase_approach.Models.ViewModels;

namespace Entity_Framework_Using_DataBase_approach.Services.InterFaces
{
    public interface ICategoryService
    {
        Task<List<CategoryDTO>> GetAllCategoriesAsync();
        Task<CategoryDTO?> GetCategoryByIdAsync(int id);
        Task<bool> CategoryExistsAsync(string categoryName, int? idToExclude = null);
        Task<CategoryDTO> CreateCategoryAsync(CategoryDTO dto);
        Task<bool> UpdateCategoryAsync(CategoryDTO dto);
        Task<bool> DeleteCategoryAsync(int id);
        Task<int> GetTotalCategoriesAsync();
    }
}
