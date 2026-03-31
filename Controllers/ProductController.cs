using Microsoft.AspNetCore.Mvc;
using Product_API.Models;

namespace Product_API.Controllers
{
    [ApiController]
    [Route("products")]
    public class ProductController : ControllerBase
    {
        private static List<Product> products = new List<Product>
        {
            new Product { Id=1, Name="Chair", Price=655.56},
            new Product { Id=2, Name="Table", Price=2555.25}
        };

        [HttpGet]
        public IActionResult GetAllProducts()
        {
            return Ok(products);
        }

        [HttpGet("{id}")]
        public IActionResult GetProductById(int id)
        {
            Product? product = products.FirstOrDefault(p => p.Id == id);

            if(product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }

        [HttpPost]
        public IActionResult AddProduct(Product product)
        {
            products.Add(product);

            return CreatedAtAction(nameof(GetProductById), new{id=product.Id}, product);
        }
    }
}
