using Product_API.Models;

namespace Product_API.IRepository
{
    public interface IOrderRepository
    {
        Order AddOrder(Order order);

        List<Order> GetAllOrders();
    }
}