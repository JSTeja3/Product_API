using Product_API.Models;
using Product_API.IServices;
using Product_API.IRepository;

namespace Product_API.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepo;
        private readonly IProductRepository _productRepo;
        private readonly IStockService _stockService;
        private readonly ILogger<OrderService> _logger;

        private static readonly object _stockLock = new();

        public OrderService(IOrderRepository orderRepo, IProductRepository productRepo, IStockService stockService, ILogger<OrderService> logger)
        {
            _orderRepo = orderRepo;
            _productRepo = productRepo;
            _stockService = stockService;
            _logger = logger;
        }

        public async Task<Order?> PlaceOrderAsync(int productId, int quantity)
        {
            _logger.LogInformation( "Order placement started for ProductId {ProductId} Quantity {Quantity} at {Timestamp}", productId, quantity, DateTime.UtcNow);

            Product? product = await _productRepo.GetProductByIdAsync(productId);

            if (product == null)
            {
                _logger.LogWarning("Order failed. Product not found for ProductId {ProductId}", productId);

                return null;
            }

            lock (_stockLock)
            {
                if (product.Stock <= 0 || product.Stock < quantity)
                {
                    _logger.LogWarning("Order rejected due to insufficient stock for ProductId {ProductId}. Requested {Quantity}, Available {AvailableStock}", productId, quantity, product.Stock);
                    return null;
                }

                product.UpdateStock(product.Stock-quantity);
                product.UpdatedAt = DateTime.UtcNow;
                _logger.LogInformation("Stock reduced for ProductId {ProductId}. RemainingStock {RemainingStock}", productId, product.Stock);


            }


            var order = new Order
            {
                OrderId = 55,
                ProductId = productId,
                Quantity = quantity,
                Status = "Placed"

            };
            var createdOrder = await _orderRepo.AddOrderAsync(order);

            _logger.LogInformation("Order created successfully for ProductId {ProductId} Quantity {Quantity} Status {Status}",productId, quantity, order.Status);

            return createdOrder;
        }

    }
}