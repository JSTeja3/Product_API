using Product_API.Models;
using Product_API.IServices;
using Product_API.IRepository;

namespace Product_API.Services
{
    public class ProductService : IProductService
    {
        private IProductRepository _repository;
        private IProductCacheService _cacheService;

        public ProductService(IProductRepository repository, IProductCacheService cacheService)
        {
            _repository = repository;
            _cacheService = cacheService;
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
                Console.WriteLine($"CACHE HIT for product {id}");
                return cachedProduct;
            }

            Console.WriteLine($"CACHE MISS for product {id}");

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