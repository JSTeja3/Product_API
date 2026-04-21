using Product_API.Models;
using Product_API.Services.Interface;

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