using Product_API.Models;
using Product_API.Interfaces.Services;
using Product_API.Interfaces.Repositories;

namespace Product_API.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepo;
        private readonly IProductRepository _productRepo;
        private readonly ILogger<OrderService> _logger;

        private static readonly object _stockLock = new();

        public OrderService(IOrderRepository orderRepo, IProductRepository productRepo, ILogger<OrderService> logger)
        {
            _orderRepo = orderRepo;
            _productRepo = productRepo;
            _logger = logger;
        }

        public async Task<List<Order>> GetOrdersAsync()
        {
            return  await _orderRepo.GetAllOrdersAsync();
        } 

        public async Task<Order?> PlaceOrderAsync(int productId, int quantity)
        {
            // _logger.LogInformation( "Order placement started for ProductId {ProductId} Quantity {Quantity} at {Timestamp}", productId, quantity, DateTime.UtcNow);

            // Product? product = await _productRepo.GetProductByIdAsync(productId);

            // if (product == null)
            // {
            //     _logger.LogWarning("Order failed. Product not found for ProductId {ProductId}", productId);

            //     return null;
            // }


            var order = new Order
            {
                OrderId = new Random().Next(),
                Quantity = quantity,
                Status = "Placed",
                ProductId = productId

            };
            var createdOrder = await _orderRepo.AddOrderAsync(order);

            _logger.LogInformation("Order created successfully for ProductId {ProductId} Quantity {Quantity} Status {Status}",productId, quantity, order.Status);

            return createdOrder;
        }

    }
}