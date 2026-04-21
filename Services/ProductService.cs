using Product_API.Models;
using Product_API.Services.Interface;
using Product_API.Repository.Interface;

namespace Product_API.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repository;
        private readonly IProductCacheService _cacheService;
        private readonly ILogger<ProductService> _logger;

        public ProductService(IProductRepository repository, IProductCacheService cacheService, ILogger<ProductService> logger)
        {
            _repository = repository;
            _cacheService = cacheService;
            _logger = logger;
        }
        public async Task<List<Product>> GetAllProductsAsync()
        {
            return await _repository.GetAllProductsAsync();
        }
        public async Task<Product?> GetProductByIdAsync(int id)
        {
            var cachedProduct = _cacheService.Get(id);

            if (cachedProduct != null)
            {
                _logger.LogInformation("Cache hit for ProductId {ProductId}",id);
                return cachedProduct;
            }

            _logger.LogInformation("Cache miss for ProductId {ProductId}",id);

            var product = await _repository.GetProductByIdAsync(id);

            if (product != null)
            {
                _cacheService.Set(product);
            }

            return product;

        }

        public async Task<Product> AddProductAsync(Product product)
        {
            return await _repository.AddProductAsync(product);
        }
        public async Task<List<Product>> SearchProductByNameAsync(string name)
        {
            return await _repository.SearchProductByNameAsync(name);
        }

        public async Task<Product?> UpdateProductAsync(int id, Product product)
        {
            var updatedProduct = await _repository.UpdateProductAsync(id, product);
            if(updatedProduct != null)
            {
                _cacheService.Remove(id);
                _cacheService.Set(updatedProduct);
                _logger.LogInformation("Cache updated for ProductId {ProductId}",id);
            }

            return updatedProduct;
        }

        public async Task<bool> DeleteProductAsync(int id)
        {
            return await _repository.DeleteProductAsync(id);
        }

        public async Task<PagedResponse<Product>> GetProductsAsync(int pageNumber, int pageSize, string? category, double? minPrice, double? maxPrice)
        {
            return await _repository.GetProductsAsync(pageNumber, pageSize, category, minPrice, maxPrice);
        }
    }
}