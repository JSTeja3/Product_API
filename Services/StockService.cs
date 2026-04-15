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
        public async Task<bool> IsInStockAsync(int productId)
        {
            Product? product = await _repository.GetProductByIdAsync(productId);

            if(product == null || !product.IsInStock())
            {
                return false;
            }
            return true;
        }

        public async Task<Product?> UpdateStockAsync(int productId, int quantity)
        {
            Product? product = await _repository.GetProductByIdAsync(productId);
            if(product != null && quantity>=0)
            {
                product.UpdateStock(quantity);
                product.UpdatedAt = DateTime.Now;
            }
            return product;
        }
    }
}