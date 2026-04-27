using Product_API.Models;

namespace Product_API.Services.Interface
{
    public interface IDiscountService
    {
        double ApplyDiscount(Product product);
    }
}