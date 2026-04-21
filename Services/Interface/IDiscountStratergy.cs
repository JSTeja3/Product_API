using Product_API.Models;

namespace Product_API.Services.Interface
{
    public interface IDiscountStrategy
    {
        double ApplyDiscount(Product product);
    }
}