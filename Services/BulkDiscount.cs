using Product_API.Models;
using Product_API.IServices;

namespace Product_API.Services
{
    public class BulkDiscount : IDiscountStrategy
    {
        public double ApplyDiscount(Product product)
        {
            return product.Price * 0.8;
        }
    }
}