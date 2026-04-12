using Product_API.Models;
using Product_API.IRepository;

namespace Product_API.Repository
{
    public class OrderRepository: IOrderRepository
    {
        List<Order> orders = new();

        public Order AddOrder(Order order)
        {
            orders.Add(order);

            return order;
        }

        public List<Order> GetAllOrders()
        {
            return orders;
        }
    }
}