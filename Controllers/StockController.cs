using Microsoft.AspNetCore.Mvc;
using Product_API.Models;
using Product_API.IServices;

namespace Product_API.Controllers
{
    [ApiController]
    [Route("stocks")]
    public class StockController:ControllerBase
    {
        private IStockService _stockService;

        public StockController(IStockService stockService)
        {
            _stockService = stockService;
        }

        [HttpGet("{id}/availability")]
        public IActionResult IsInStock(int id)
        {
            bool available = _stockService.IsInStock(id);

            return Ok(new
            {
                productId = id,
                isAvailable = available
            });
        }

        [HttpGet("{id}/update")]
        public IActionResult UpdateStock(int id, [FromQuery]int quantity)
        {
            Product? product = _stockService.UpdateStock(id, quantity);
            if(product == null)
            {
                return Conflict("Product doesn't exist or requested quantity is not available");
            }

            return Ok(product);
        }
    }
}