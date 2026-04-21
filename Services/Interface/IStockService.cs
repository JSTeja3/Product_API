using Product_API.Models;

namespace Product_API.Services.Interface
{
    public interface IStockService
    {
        Task<bool> IsInStockAsync(int productId);

        Task<Product?> UpdateStockAsync(int productId, int quantity);
    }
}