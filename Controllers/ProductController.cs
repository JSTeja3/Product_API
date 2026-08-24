using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Product_API.Models;
using Product_API.Interfaces.Services;
using Product_API.DTOs.Requests;
using Product_API.DTOs.Responses;


namespace Product_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
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

        //[Authorize(Roles="Admin")]
        [HttpPost("create")]
        public async Task<IActionResult> CreateAsync(CreateProductRequest request)
        {
            CreateProductResponse createdProduct =
                await _productService.CreateAsync(request);

            return CreatedAtAction(
                "GetProductByProductId",
                new { productId = createdProduct.ProductId },
                createdProduct);
        }

        //[Authorize]
        [HttpGet("{productId}")]
        public async Task<IActionResult> GetProductByProductIdAsync(string productId)
        {
            GetProductResponse? product =
                await _productService.GetProductByProductIdAsync(productId);

            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        [Authorize]
        [HttpGet("search")]
        public async Task<IActionResult> SearchProductByName(string name)
        {
            List<Product> products = await this._productService.SearchProductByNameAsync(name);
            return Ok(products);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, Product product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Product? updateProduct = await this._productService.UpdateProductAsync(id, product);

            if (updateProduct == null)
            {
                return NotFound();
            }
            return Ok(updateProduct);
        }

        [Authorize(Roles = "Admin")]
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
        public async Task<IActionResult> GetProducts([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10, [FromQuery] string? category = null)
        {
            var result = await _productService.GetProductsAsync(pageNumber, pageSize, category);

            return Ok(result);
        }
    }
}
