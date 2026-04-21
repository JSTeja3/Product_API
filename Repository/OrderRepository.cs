using Product_API.Models;
using Product_API.IRepository;
using Product_API.Data;
using Microsoft.EntityFrameworkCore;

namespace Product_API.Repository
{
    public class OrderRepository: IOrderRepository
    {
        private readonly AppDbContext _dbContext;

        public OrderRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Order> AddOrderAsync(Order order)
        {
            await _dbContext.Orders.AddAsync(order);
            await _dbContext.SaveChangesAsync();

            return order;
        }

        public async Task<List<Order>> GetAllOrdersAsync()
        {
            var orders = await _dbContext.Orders.Include(o=>o.Product).AsNoTracking().ToListAsync();
            return orders;
        }
    }
}