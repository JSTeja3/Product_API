using Product_API.Models;

namespace Product_API.DTOs.Responses
{
    public class CreateProductResponse
    {
        public int Id { get; set; }
        public string ProductId { get; set; } = string.Empty;

        public string ProductName { get; set; } = string.Empty;

        public string Category { get; set; } = string.Empty;

        public int Quantity { get; set; }

        public int Version { get; set; }

        public ProductState State { get; set; }

        public DateTime CreatedAt { get; set; }

    }
}