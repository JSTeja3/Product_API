using Product_API.Models;

namespace Product_API.Services.Interface
{
    public interface IOrderService
    {
        Task<List<Order>> GetOrdersAsync();
        Task<Order?> PlaceOrderAsync(int productId, int quantity);   
    }
}