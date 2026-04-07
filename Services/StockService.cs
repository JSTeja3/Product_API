using Product_API.Models;
using Product_API.IServices;

namespace Product_API.Services
{
    public class StockService:IStockService
    {

        private IProductService _productService;

        public StockService(IProductService productService)
        {
            _productService = productService;
        }
        public bool IsInStock(int productId)
        {
            Product? product = _productService.GetProductById(productId);

            if(product == null || !product.IsInStock())
            {
                return false;
            }
            return true;
        }

        public Product? UpdateStock(int productId, int quantity)
        {
            Product? product = _productService.GetProductById(productId);
            if(product != null)
            {
                product.UpdateStock(quantity);
            }
            return product;
        }
    }
}