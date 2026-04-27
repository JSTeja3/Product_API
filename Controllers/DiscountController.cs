using Microsoft.AspNetCore.Mvc;
using Product_API.Models;
using Product_API.Services.Interface;

namespace Product_API.Controllers
{
    [ApiController]
    [Route("discounts")]
    public class DiscountController : ControllerBase
    {
        private readonly IDiscountService _discountService;
        private readonly IProductService _productService;

        public DiscountController(IDiscountService discountService, IProductService productService)
        {
            _discountService = discountService;
            _productService = productService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDiscountedPrice(int id)
        {
            Product? product = await _productService.GetProductByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            double discountedPrice = _discountService.ApplyDiscount(product);

            return Ok(new
            {
                isDiscounted = product.Price != discountedPrice,
                newPrice = discountedPrice
                
            });
        }
        
    }
}