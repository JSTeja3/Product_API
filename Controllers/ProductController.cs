using Microsoft.AspNetCore.Mvc;
using Product_API.Models;
using Product_API.IServices;
using Product_API.Services;

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
            Product createdProduct = this._productService.AddProduct(product);

            return CreatedAtAction(nameof(GetProductById), new{id=product.Id}, createdProduct);
        }

        [HttpGet("search")]
        public IActionResult SearchProductByName(string name)
        {
            List<Product> products = this._productService.SearchProductByName(name);
            return Ok(products);
        }
    }
}
