using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Product_API.Models
{
    public class Product
    {
        [DefaultValue(0)]
        public int Id{ get; set;}

        [Required(ErrorMessage = "Product Name is Required")]
        [DefaultValue("string")]
        public required String Name{get; set;}

        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than 0")]
        public double Price{get; set;}

        [DefaultValue("string")]
        public required string Category{get; set;} 

        [Range(0, int.MaxValue, ErrorMessage = "Stock cannot be negative number")]
        [DefaultValue(0)]
        public int Stock{get; set;}
    }
}