using Product_API.Models;
using Product_API.DTOs.Requests;
using Product_API.DTOs.Responses;

namespace Product_API.Interfaces.Services
{
    public interface IProductService
    {
        Task<List<Product>> GetAllProductsAsync();

        Task<Product?> GetProductByIdAsync(int id);

        Task<CreateProductResponse> CreateAsync(CreateProductRequest request);

        Task<List<Product>> SearchProductByNameAsync(string name); 

        Task<Product?> UpdateProductAsync(int id, Product product);

        Task<bool> DeleteProductAsync(int id);

        Task<PagedResponse<Product>> GetProductsAsync(int pageNumber, int pageSize, string? category);
        
    }
}