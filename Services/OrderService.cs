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

        public async Task<Order?> PlaceOrderAsync(int productId, int quantity)
        {
            Product? product = await _productRepo.GetProductByIdAsync(productId);

            if (product == null)
            {
                return null;
            }

            if ( product.Stock <= 0 || product.Stock < quantity)
            {
                return null;
            }

            await _stockService.UpdateStockAsync(productId, product.Stock-quantity);

            var order = new Order
            {
               ProductId = productId,
               Quantity = quantity,
               Status = "Placed"

            };
            return await _orderRepo.AddOrderAsync(order);
        }

    }
}