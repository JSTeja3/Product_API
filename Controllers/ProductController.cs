using Microsoft.AspNetCore.Mvc;
using Product_API.Models;
using Product_API.Services.Interface;
using Microsoft.AspNetCore.Authorization;


namespace Product_API.Controllers
{
    [ApiController]
    [Route("products")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService; 
        public ProductController(IProductService productService)
        {
            this._productService = productService;   
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            var products = await this._productService.GetAllProductsAsync();
            return Ok(products);
            //throw new InvalidOperationException("Database connection timeout");
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            Product? product = await this._productService.GetProductByIdAsync(id);

            if(product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }

        [Authorize(Roles="Admin")]
        [HttpPost]
        public async Task<IActionResult> AddProduct(Product product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Product createdProduct = await this._productService.AddProductAsync(product);

            return CreatedAtAction(nameof(GetProductById), new{id=product.Id}, createdProduct);
        }

        [Authorize]
        [HttpGet("search")]
        public async Task<IActionResult> SearchProductByName(string name)
        {
            List<Product> products = await this._productService.SearchProductByNameAsync(name);
            return Ok(products);
        }

        [Authorize(Roles="Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, Product product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Product? updateProduct = await this._productService.UpdateProductAsync(id, product);

            if(updateProduct == null)
            {
                return NotFound();
            }
            return Ok(updateProduct);
        }

        [Authorize(Roles="Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            bool isDeleted = await _productService.DeleteProductAsync(id);
            if (!isDeleted)
            {
                return NotFound();
            }

            return NoContent();
        } 

        [Authorize]
        [HttpGet("filters")]
        public async Task<IActionResult> GetProducts([FromQuery] int pageNumber=1, [FromQuery] int pageSize=10, [FromQuery] string? category=null, [FromQuery] double? minPrice=null, [FromQuery] double? maxPrice=null)
        {
            var result = await _productService.GetProductsAsync(pageNumber, pageSize, category, minPrice, maxPrice);

            return Ok(result);
        }
    }
}
