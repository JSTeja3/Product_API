using Product_API.Models;
using Product_API.DTOs.Requests;
using Product_API.DTOs.Responses;

namespace Product_API.Interfaces.Services
{
    public interface IProductService
    {
        Task<List<Product>> GetAllProductsAsync();

        Task<GetProductResponse?> GetProductByProductIdAsync(string productId);

        Task<CreateProductResponse> CreateAsync(CreateProductRequest request);

        Task<List<Product>> SearchProductByNameAsync(string name); 

        Task<GetProductResponse?> UpdateAsync(string productId, UpdateProductRequest request);

        Task<bool> DeleteProductAsync(int id);

        Task<PagedResponse<Product>> GetProductsAsync(int pageNumber, int pageSize, string? category);
        
    }
}