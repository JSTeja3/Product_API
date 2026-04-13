using Product_API.Models;

namespace Product_API.IServices
{
    public interface IProductCacheService
    {
        Product? Get(int productId);

        void Set(Product product);

        void Remove(int ProductId);
    }
}