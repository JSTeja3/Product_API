using Product_API.Models;

namespace Product_API.IServices
{
    public interface IProductService
    {
        List<Product> GetAllProducts();

        Product? GetProductById(int id);

        Product AddProduct(Product product);
    }
}