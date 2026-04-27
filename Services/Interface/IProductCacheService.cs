using Product_API.Models;

namespace Product_API.Services.Interface
{
    public interface IProductCacheService
    {
        Product? Get(int productId);

        void Set(Product product);

        void Remove(int ProductId);
    }
}