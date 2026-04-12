namespace Product_API.Models
{
    public class Order : BaseEntity
    {
        public int ProductId {get; set;}

        public int Quantity {get; set;}

        public string Status{get; set;} = "Placed";
    }
}