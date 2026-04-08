using Product_API.Models;

namespace Product_API.IServices
{
    public interface IDiscountStrategy
    {
        double ApplyDiscount(Product product);
    }
}