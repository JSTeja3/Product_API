using Product_API.Models;

namespace Product_API.Interfaces.Repositories
{
    public interface IProductRepository
    {
        Task<string?> GetLatestProductIdAsync(int year);

        Task<List<Product>> GetAllProductsAsync();

        Task<Product?> GetProductByProductIdAsync(string productId);

        Task<Product> CreateAsync(Product product);

        Task<List<Product>> SearchProductByNameAsync(string name); 

        Task<Product?> UpdateProductAsync(int id, Product product);

        Task<bool> DeleteProductAsync(int id);

        Task<PagedResponse<Product>> GetProductsAsync(int pageNumber, int pageSize, string? category); 
    }
}