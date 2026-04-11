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

        public OrderService(IOrderRepository orderRepo, IProductRepository productRepo, IStockService stockService)
        {
            _orderRepo = orderRepo;
            _productRepo = productRepo;
            _stockService = stockService;
        }

        public Order? PlaceOrder(int productId, int quantity)
        {
            Product? product = _productRepo.GetProductById(productId);

            if (product == null)
            {
                return null;
            }

            if (product.Stock < quantity)
            {
                return null;
            }

            _stockService.UpdateStock(productId, product.Stock-quantity);

            var order = new Order
            {
               ProductId = productId,
               Quantity = quantity,
               Status = "Placed"

            };
            return _orderRepo.AddOrder(order);
        }

    }
}