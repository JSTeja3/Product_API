using Microsoft.AspNetCore.Mvc;

using Product_API.Models;
using Product_API.IServices;

namespace Product_API.Controllers
{
    [ApiController]
    [Route("orders")]
    public class OrderController: ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost]
        public IActionResult PlaceOrder([FromQuery]int productId, [FromQuery]int quantity)
        {
            Order? order = _orderService.PlaceOrder(productId, quantity);
            if (order == null)
            {
                return Conflict("Insufficient stock or product invalid");
            }
            return Ok(order);
        }
    }
}