using Product_API.Models;

namespace Product_API.IRepository
{
    public interface IProductRepository
    {
        List<Product> GetAllProducts();

        Product? GetProductById(int id);

        Product AddProduct(Product product);

        List<Product> SearchProductByName(string name); 

        Product? UpdateProduct(int id, Product product);

        bool DeleteProduct(int id);

        PagedResponse<Product> GetProducts(int pageNumber, int pageSize, string? category, double? minPrice, double? maxPrice); 
    }
}