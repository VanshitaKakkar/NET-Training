using Entity_Framework_Using_DataBase_approach.Models.ViewModels;

namespace Entity_Framework_Using_DataBase_approach.Services.InterFaces
{
    public interface IProductService
    {
        Task<int> GetTotalProductsAsync();
        Task<List<CategoryDTO>> GetAllCategoriesAsync();

        Task<List<ProductDTO>> FilterProductsAsync(int? categoryId, string search, string sortOrder);
        Task<ProductDTO?> GetProductByIdAsync(int id);
        Task<bool> ProductExistsAsync(string name, int? excludeId = null);
        Task AddProductAsync(ProductDTO dto);
        Task UpdateProductAsync(ProductDTO dto);
        Task DeleteProductAsync(int id);
    }
}
