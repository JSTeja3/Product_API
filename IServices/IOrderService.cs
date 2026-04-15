using Product_API.Models;

namespace Product_API.IServices
{
    public interface IOrderService
    {
        Task<Order?> PlaceOrderAsync(int productId, int quantity);
    }
}