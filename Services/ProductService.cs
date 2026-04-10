using Product_API.Models;
using Product_API.IServices;

namespace Product_API.Services
{
    public class ProductService : IProductService
    {
        private IProductRepository _repository;

        public ProductService(IProductRepository repository)
        {
            _repository = repository;
        }
        public List<Product> GetAllProducts()
        {
            return _repository.GetAllProducts();
        }
        public Product? GetProductById(int id)
        {
            return _repository.GetProductById(id);
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
            return _repository.UpdateProduct(id, product);
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