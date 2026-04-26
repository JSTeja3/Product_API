using Microsoft.AspNetCore.Mvc;

using Product_API.Models;
using Product_API.Services.Interface;
using Microsoft.AspNetCore.Authorization;

namespace Product_API.Controllers
{
    [ApiController]
    [Route("orders")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetOrders(){
            var orders = await _orderService.GetOrdersAsync();
            return Ok(orders);
        }


        [Authorize]
        [HttpPost]
        public async Task<IActionResult> PlaceOrder([FromQuery] int productId, [FromQuery] int quantity)
        {
            Order? order = await _orderService.PlaceOrderAsync(productId, quantity);
            if (order == null)
            {
                return Conflict("Insufficient stock or product invalid");
            }
            return Ok(order);
        }

        [Authorize(Roles="Admin")]
        [HttpPost("simulate-concurrent-orders")]
        public async Task<IActionResult> SimulateConcurrentOrders()
        {
            var task1 = _orderService.PlaceOrderAsync(5, 1);
            var task2 = _orderService.PlaceOrderAsync(5, 1);
            var task3 = _orderService.PlaceOrderAsync(5, 1);

            var results = await Task.WhenAll(task1, task2, task3);

            var successCount = results.Count(r => r != null);

            return Ok(new
            {
                successfulOrders = successCount
            });
        }
    }
}