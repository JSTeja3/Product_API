using Product_API.Models;

namespace Product_API.IServices
{
    public interface IStockService
    {
        bool IsInStock(int productId);

        Product? UpdateStock(int productId, int quantity);
    }
}