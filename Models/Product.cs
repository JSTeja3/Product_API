namespace Product_API.Models
{
    public class Product
    {
        public int Id{ get; set;}
        public required String Name{get; set;}

        public double Price{get; set;}

        public required string Category{get; set;}
        public int Stock{get; set;}
    }
}