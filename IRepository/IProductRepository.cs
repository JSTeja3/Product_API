using Product_API.Models;

namespace Product_API.IRepository
{
    public interface IProductRepository
    {
        Task<List<Product>> GetAllProductsAsync();

        Task<Product?> GetProductByIdAsync(int id);

        Task<Product> AddProductAsync(Product product);

        Task<List<Product>> SearchProductByNameAsync(string name); 

        Task<Product?> UpdateProductAsync(int id, Product product);

        Task<bool> DeleteProductAsync(int id);

        Task<PagedResponse<Product>> GetProductsAsync(int pageNumber, int pageSize, string? category, double? minPrice, double? maxPrice); 
    }
}