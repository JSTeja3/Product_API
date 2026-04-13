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
        public List<Product> GetAllProducts()
        {
            return _repository.GetAllProducts();
        }
        public Product? GetProductById(int id)
        {
            var cachedProduct = _cacheService.Get(id);

            if (cachedProduct != null)
            {
                Console.WriteLine($"CACHE HIT for product {id}");
                return cachedProduct;
            }

            Console.WriteLine($"CACHE MISS for product {id}");

            var product = _repository.GetProductById(id);

            if (product != null)
            {
                _cacheService.Set(product);
            }

            return product;

        }

        public Product AddProduct(Product product)
        {
            return _repository.AddProduct(product);
        }
        public List<Product> SearchProductByName(string name)
        {
            return _repository.SearchProductByName(name);
        }

        public Product? UpdateProduct(int id, Product product)
        {
            var updatedProduct = _repository.UpdateProduct(id, product);
            if(updatedProduct != null)
            {
                _cacheService.Remove(id);
                _cacheService.Set(updatedProduct);
            }

            return updatedProduct;
        }

        public bool DeleteProduct(int id)
        {
            return _repository.DeleteProduct(id);
        }

        public PagedResponse<Product> GetProducts(int pageNumber, int pageSize, string? category, double? minPrice, double? maxPrice)
        {
            return _repository.GetProducts(pageNumber, pageSize, category, minPrice, maxPrice);
        }
    }
}