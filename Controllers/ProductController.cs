using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Product_API.Models;
using Product_API.Interfaces.Services;
using Product_API.DTOs.Requests;
using Product_API.DTOs.Responses;


namespace Product_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
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
        [HttpPost]
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

            return Ok(product);
        }

        //[Authorize(Roles = "Admin")]
        [HttpPatch("{productId}")]
        public async Task<IActionResult> UpdateAsync(string productId, UpdateProductRequest request)
        {
            GetProductResponse? updatedProduct =
                await _productService.UpdateAsync(productId, request);

            return Ok(updatedProduct);
        }

        [Authorize]
        [HttpGet("search")]
        public async Task<IActionResult> SearchProductByName(string name)
        {
            List<Product> products = await this._productService.SearchProductByNameAsync(name);
            return Ok(products);
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
