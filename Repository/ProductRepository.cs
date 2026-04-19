using Product_API.Models;
using Product_API.IRepository;
using Product_API.Data;
using Microsoft.EntityFrameworkCore;

namespace Product_API.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext _dbContext;

        public ProductRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<List<Product>> GetAllProductsAsync()
        {
            return await _dbContext.Products.ToListAsync();
        }
        public async Task<Product?> GetProductByIdAsync(int id)
        {
            return await _dbContext.Products.FindAsync(id);
        }

        public async Task<Product> AddProductAsync(Product product)
        {
            await _dbContext.Products.AddAsync(product);
            await _dbContext.SaveChangesAsync();

            return product;
        }
        public async Task<List<Product>> SearchProductByNameAsync(string name)
        {
            var products = await _dbContext.Products.Where(p => p.Name.ToLower().Contains(name.ToLower())).ToListAsync();
            return products;
        }

        public async Task<Product?> UpdateProductAsync(int id, Product product)
        {
            await Task.Delay(50);

            var existingProduct = await _dbContext.Products.FindAsync(product.Id);

            if (existingProduct == null)
            {
                return null;
            }
            existingProduct.Name = product.Name;
            existingProduct.Price = product.Price;
            existingProduct.Category = product.Category;
            existingProduct.Stock = product.Stock;
            existingProduct.UpdatedAt = DateTime.Now;

            await _dbContext.SaveChangesAsync();

            return existingProduct;
        }

        public async Task<bool> DeleteProductAsync(int id)
        {
            var existingProduct = await _dbContext.Products.FindAsync(id);
            if (existingProduct == null)
            {
                return false;
            }

            _dbContext.Products.Remove(existingProduct);
            await _dbContext.SaveChangesAsync();



            return true;
        }

        public async Task<PagedResponse<Product>> GetProductsAsync(int pageNumber, int pageSize, string? category, double? minPrice, double? maxPrice)
        {
            var query = _dbContext.Products.AsNoTracking().AsQueryable();
            if (!string.IsNullOrWhiteSpace(category))
            {
                query = query.Where(p =>
                    p.Category.ToLower() == category.ToLower());
            }

            if (minPrice.HasValue)
            {
                query = query.Where(p => p.Price >= minPrice.Value);
            }

            if (maxPrice.HasValue)
            {
                query = query.Where(p => p.Price <= maxPrice.Value);
            }

            var totalCount = await query.CountAsync();

            var pagedProducts = await query.Skip((pageNumber-1)*pageSize).Take(pageSize).ToListAsync();

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