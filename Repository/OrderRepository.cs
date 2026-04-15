using Product_API.Models;
using Product_API.IRepository;

namespace Product_API.Repository
{
    public class OrderRepository: IOrderRepository
    {
        List<Order> orders = new();

        public async Task<Order> AddOrderAsync(Order order)
        {
            await Task.Delay(50);
            orders.Add(order);

            return order;
        }

        public async Task<List<Order>> GetAllOrdersAsync()
        {
            return orders;
        }
    }
}