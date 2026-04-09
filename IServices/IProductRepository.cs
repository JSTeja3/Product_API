using Product_API.Models;

namespace Product_API.IServices
{
    public interface IProductRepository
    {
        List<Product> GetAllProducts();

        Product? GetProductById(int id);

        Product AddProduct(Product product);

        List<Product> SearchProductByName(string name); 

        Product? UpdateProduct(int id, Product product);

        bool DeleteProduct(int id);
    }
}