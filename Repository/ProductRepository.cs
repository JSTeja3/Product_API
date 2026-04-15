using Product_API.Models;
using Product_API.IRepository;

namespace Product_API.Repository
{
    public class ProductRepository : IProductRepository
    {
        private static List<Product> products = new List<Product>
        {
            new Product { Id=1, Name="Chair-WH001-35X25X60", Price=655.56, Category="Dining", Stock=25},
            new Product { Id=2, Name="Table-BL001-105X102X45", Price=2555.25, Category="Dining", Stock=5},
            new Product { Id=3, Name="Lamp-BE001-6X15", Price=230.23, Category="Bed", Stock=0}

        };
        public async Task<List<Product>> GetAllProductsAsync()
        {
            await Task.Delay(100);
            return products.ToList();
        }
        public async Task<Product?> GetProductByIdAsync(int id)
        {
            await Task.Delay(50);
            Product? product = products.FirstOrDefault(p => p.Id == id);
            return product;
        }

        public async Task<Product> AddProductAsync(Product product)
        {
            products.Add(product);
            return product;
        }
        public async Task<List<Product>> SearchProductByNameAsync(string name)
        {
            List<Product> result = products.Where(p => p.Name.Contains(name, StringComparison.OrdinalIgnoreCase)).ToList();
            return result;
        }

        public async Task<Product?> UpdateProductAsync(int id, Product product)
        {
            await Task.Delay(50);

            Product? existingProduct = products.FirstOrDefault(p => p.Id == id);

            if (existingProduct == null)
            {
                return null;
            }
            existingProduct.Name = product.Name;
            existingProduct.Price = product.Price;
            existingProduct.Category = product.Category;
            existingProduct.Stock = product.Stock;
            existingProduct.UpdatedAt = DateTime.Now;

            return existingProduct;
        }

        public async Task<bool> DeleteProductAsync(int id)
        {
            var existingProduct = products.FirstOrDefault(p => p.Id == id);
            if (existingProduct == null)
            {
                return false;
            }

            products.Remove(existingProduct);

            return true;
        }

        public async Task<PagedResponse<Product>> GetProductsAsync(int pageNumber, int pageSize, string? category, double? minPrice, double? maxPrice)
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