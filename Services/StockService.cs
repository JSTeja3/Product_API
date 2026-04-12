using Product_API.Models;
using Product_API.IServices;
using Product_API.IRepository;

namespace Product_API.Services
{
    public class StockService:IStockService
    {

        private IProductRepository _repository;

        public StockService(IProductRepository repository)
        {
            _repository = repository;
        }
        public bool IsInStock(int productId)
        {
            Product? product = _repository.GetProductById(productId);

            if(product == null || !product.IsInStock())
            {
                return false;
            }
            return true;
        }

        public Product? UpdateStock(int productId, int quantity)
        {
            Product? product = _repository.GetProductById(productId);
            if(product != null && quantity>=0)
            {
                product.UpdateStock(quantity);
                product.UpdatedAt = DateTime.Now;
            }
            return product;
        }
    }
}