using Product_API.Models;
using Product_API.Interfaces.Services;
using Product_API.Interfaces.Repositories;
using Product_API.DTOs.Requests;
using Product_API.DTOs.Responses;

namespace Product_API.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repo;
        private readonly IProductCacheService _cacheService;
        private readonly ILogger<ProductService> _logger;

        public ProductService(IProductRepository repo, IProductCacheService cacheService, ILogger<ProductService> logger)
        {
            _repo = repo;
            _cacheService = cacheService;
            _logger = logger;
        }
        public async Task<List<Product>> GetAllProductsAsync()
        {
            return await _repo.GetAllProductsAsync();
        }
        public async Task<GetProductResponse?> GetProductByProductIdAsync(string productId)
        {
            // var cachedProduct = _cacheService.Get(id);

            // if (cachedProduct != null)
            // {
            //     _logger.LogInformation("Cache hit for ProductId {ProductId}", id);
            //     return cachedProduct;
            // }

            // _logger.LogInformation("Cache miss for ProductId {ProductId}", id);

            Product? product = await _repo.GetProductByProductIdAsync(productId);

            if (product == null)
            {
                return null;
            }

            return new GetProductResponse
            {
                ProductId = product.ProductId,
                ProductName = product.ProductName,
                Category = product.Category,
                Quantity = product.Quantity,
                Version = product.Version,
                State = product.State,
                CreatedAt = product.CreatedAt,
                UpdatedAt = product.UpdatedAt
            };

        }

        public async Task<CreateProductResponse> CreateAsync(CreateProductRequest request)
        {
            int currentYear = DateTime.UtcNow.Year;

            string? latestProductId = await _repo.GetLatestProductIdAsync(currentYear);

            int nextNumber = 1;

            if (!string.IsNullOrEmpty(latestProductId))
            {
                string[] parts = latestProductId.Split('-');

                nextNumber = int.Parse(parts[1]) + 1;
            }

            string productId = $"{currentYear}-{nextNumber:D4}";

            Product product = new Product
            {
                ProductId = productId,
                ProductName = request.ProductName,
                Category = request.Category,
                Quantity = request.Quantity,

                Version = 1,
                State = ProductState.Draft,

                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow

            };

            Product createdProduct = await _repo.CreateAsync(product);

            return new CreateProductResponse
            {
                Id = createdProduct.Id,
                ProductId = createdProduct.ProductId,
                ProductName = createdProduct.ProductName,
                Category = createdProduct.Category,
                Quantity = createdProduct.Quantity,
                Version = createdProduct.Version,
                State = createdProduct.State,
                CreatedAt = createdProduct.CreatedAt
            };
        }
        public async Task<List<Product>> SearchProductByNameAsync(string name)
        {
            return await _repo.SearchProductByNameAsync(name);
        }

        public async Task<Product?> UpdateProductAsync(int id, Product product)
        {
            var updatedProduct = await _repo.UpdateProductAsync(id, product);
            if (updatedProduct != null)
            {
                _cacheService.Remove(id);
                _cacheService.Set(updatedProduct);
                _logger.LogInformation("Cache updated for ProductId {ProductId}", id);
            }

            return updatedProduct;
        }

        public async Task<bool> DeleteProductAsync(int id)
        {
            return await _repo.DeleteProductAsync(id);
        }

        public async Task<PagedResponse<Product>> GetProductsAsync(int pageNumber, int pageSize, string? category)
        {
            return await _repo.GetProductsAsync(pageNumber, pageSize, category);
        }
    }
}