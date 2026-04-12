using Product_API.Models;

namespace Product_API.IServices
{
    public interface IOrderService
    {
        Order? PlaceOrder(int productId, int quantity);
    }
}