using AutoMapper;
using Entity_Framework.Data;
using Entity_Framework_Using_DataBase_approach.Models;
using Entity_Framework_Using_DataBase_approach.Models.ViewModels;
using Entity_Framework_Using_DataBase_approach.Services.InterFaces;
using Microsoft.EntityFrameworkCore;
namespace Entity_Framework_Using_DataBase_approach.Services
{
    public class ProductService : IProductService
    {
        private readonly MyDbContext _dbContext;
        private readonly IMapper _mapper;

        public ProductService(MyDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<int> GetTotalProductsAsync()
        {
            return await _dbContext.Products.CountAsync();
        }

        public async Task<List<CategoryDTO>> GetAllCategoriesAsync()
        {
            var categories = await _dbContext.Categories.ToListAsync();
            return _mapper.Map<List<CategoryDTO>>(categories);
        }

        public async Task<List<ProductDTO>> FilterProductsAsync(int? categoryId, string search, string sortOrder)
        {
            var query = _dbContext.Products
                                  .Include(p => p.Category)
                                  .AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
                query = query.Where(p => p.Name.Contains(search));

            if (categoryId.HasValue)
                query = query.Where(p => p.CategoryId == categoryId);

            query = sortOrder switch
            {
                "price_asc" => query.OrderBy(p => p.Price),
                "price_desc" => query.OrderByDescending(p => p.Price),
                _ => query
            };

            var products = await query.ToListAsync();
            return _mapper.Map<List<ProductDTO>>(products);
        }

        public async Task<ProductDTO?> GetProductByIdAsync(int id)
        {
            var product = await _dbContext.Products.Include(p => p.Category)
                                .FirstOrDefaultAsync(p => p.ProductId == id);
            return product == null ? null : _mapper.Map<ProductDTO>(product);
        }

        public async Task<bool> ProductExistsAsync(string name, int? excludeId = null)
        {
            return await _dbContext.Products
                .AnyAsync(p => p.Name == name && (!excludeId.HasValue || p.ProductId != excludeId));
        }

        public async Task AddProductAsync(ProductDTO dto)
        {
            var product = _mapper.Map<Product>(dto);
            _dbContext.Products.Add(product);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateProductAsync(ProductDTO dto)
        {
            var product = await _dbContext.Products.FindAsync(dto.ProductId);
            if (product != null)
            {
                _mapper.Map(dto, product);
                _dbContext.Products.Update(product);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task DeleteProductAsync(int id)
        {
            var product = await _dbContext.Products.FindAsync(id);
            if (product != null)
            {
                _dbContext.Products.Remove(product);
                await _dbContext.SaveChangesAsync();
            }
        }
    }

}

