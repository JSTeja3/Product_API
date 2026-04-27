using Product_API.Models;

namespace Product_API.Repository.Interface
{
    public interface IOrderRepository
    {
        Task<Order> AddOrderAsync(Order order);

        Task<List<Order>> GetAllOrdersAsync();
    }
}