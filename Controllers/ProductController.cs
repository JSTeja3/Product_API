using Microsoft.AspNetCore.Mvc;
using Product_API.Models;
using Product_API.IServices;

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

        [HttpGet]
        public IActionResult GetAllProducts()
        {
            List<Product> products = this._productService.GetAllProducts();
            return Ok(products);
            //throw new InvalidOperationException("Database connection timeout");
        }

        [HttpGet("{id}")]
        public IActionResult GetProductById(int id)
        {
            Product? product = this._productService.GetProductById(id);

            if(product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }

        [HttpPost]
        public IActionResult AddProduct(Product product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Product createdProduct = this._productService.AddProduct(product);

            return CreatedAtAction(nameof(GetProductById), new{id=product.Id}, createdProduct);
        }

        [HttpGet("search")]
        public IActionResult SearchProductByName(string name)
        {
            List<Product> products = this._productService.SearchProductByName(name);
            return Ok(products);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateProduct(int id, Product product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Product? updateProduct = this._productService.UpdateProduct(id, product);

            if(updateProduct == null)
            {
                return NotFound();
            }
            return Ok(updateProduct);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteProduct(int id)
        {
            bool isDeleted = _productService.DeleteProduct(id);
            if (!isDeleted)
            {
                return NotFound();
            }

            return NoContent();
        } 

        [HttpGet("filters")]
        public IActionResult GetProducts([FromQuery] int pageNumber=1, [FromQuery] int pageSize=10, [FromQuery] string? category=null, [FromQuery] double? minPrice=null, [FromQuery] double? maxPrice=null)
        {
            var result = _productService.GetProducts(pageNumber, pageSize, category, minPrice, maxPrice);

            return Ok(result);
        }
    }
}
