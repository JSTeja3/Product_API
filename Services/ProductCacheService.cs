using Product_API.Models;
using Product_API.Services.Interface;

namespace Product_API.Services
{
    public class ProductCacheService: IProductCacheService
    {
        public static Dictionary<int, Product> _cachedData = new();

        public Product? Get(int productId)
        {
            _cachedData.TryGetValue(productId, out var product);
            return product;
        }
        public void Set(Product product)
        {
            _cachedData[product.Id] = product;
        }

        public void Remove(int productId)
        {
            _cachedData.Remove(productId);
        }
    }
}