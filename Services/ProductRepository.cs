using Product_API.Models;
using Product_API.IServices;

namespace Product_API.Services
{
    public class ProductRepository : IProductRepository
    {
        private static List<Product> products = new List<Product>
        {
            new Product { Id=1, Name="Chair-WH001-35X25X60", Price=655.56, Category="Dining", Stock=25},
            new Product { Id=2, Name="Table-BL001-105X102X45", Price=2555.25, Category="Dining", Stock=5},
            new Product { Id=3, Name="Lamp-BE001-6X15", Price=230.23, Category="Bed", Stock=0}

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
        public List<Product> SearchProductByName(string name)
        {
            List<Product> result = products.Where(p => p.Name.Contains(name, StringComparison.OrdinalIgnoreCase)).ToList();
            return result;
        }

        public Product? UpdateProduct(int id, Product product)
        {
            Product? existingProduct = products.FirstOrDefault(p => p.Id == id);

            if (existingProduct == null)
            {
                return null;
            }
            existingProduct.Name = product.Name;
            existingProduct.Price = product.Price;
            existingProduct.Category = product.Category;
            existingProduct.Stock = product.Stock;

            return existingProduct;
        }

        public bool DeleteProduct(int id)
        {
            var existingProduct = products.FirstOrDefault(p => p.Id == id);
            if (existingProduct == null)
            {
                return false;
            }

            products.Remove(existingProduct);

            return true;
        }

        public PagedResponse<Product> GetProducts(int pageNumber, int pageSize, string? category, double? minPrice, double? maxPrice)
        {
            var query = products.AsQueryable();
            if (!string.IsNullOrWhiteSpace(category))
            {
                query = query.Where(p =>
                    p.Category.Equals(category, StringComparison.OrdinalIgnoreCase));
            }

            if (minPrice.HasValue)
            {
                query = query.Where(p => p.Price >= minPrice.Value);
            }

            if (maxPrice.HasValue)
            {
                query = query.Where(p => p.Price <= maxPrice.Value);
            }

            var totalCount = query.Count();

            var pagedProducts = query.Skip((pageNumber-1)*pageSize).Take(pageSize).ToList();

            return new PagedResponse<Product>
            {
              TotalCount = totalCount,
              PageNumber = pageNumber,
              PageSize = pageSize,
              Data = pagedProducts
            };

        }
    }
}