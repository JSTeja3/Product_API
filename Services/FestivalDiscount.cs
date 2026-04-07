using Product_API.Models;

namespace Product_API.Services
{
    public class FestivalDiscount : IDiscountStrategy
    {
        public double ApplyDiscount(Product product)
        {
            return product.Price * 0.9;
        }
    }
}