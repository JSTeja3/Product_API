using Product_API.Models;

namespace Product_API.Services
{
    public interface IDiscountStrategy
    {
        double ApplyDiscount(Product product);
    }
}