using Product_API.Models;
using Product_API.IServices;

namespace Product_API.Services
{
    public class ProductService : IProductService
    {
         private static List<Product> products = new List<Product>
        {
            new Product { Id=1, Name="Chair", Price=655.56},
            new Product { Id=2, Name="Table", Price=2555.25}
        };
        public List<Product> GetAllProducts()
        {
            return products;
        }   
        public Product? GetProductById(int id)
        {
            Product? product = products.FirstOrDefault(p => p.Id == id);
            return product;
        }

        public Product AddProduct(Product product)
        {
            products.Add(product);
            return product;
        }
    }
}