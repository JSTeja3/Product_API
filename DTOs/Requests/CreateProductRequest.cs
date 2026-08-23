using System.ComponentModel.DataAnnotations;

namespace Product_API.DTOs.Requests
{
    public class CreateProductRequest
    {
        [Required]
        [MaxLength(200)]
        public string ProductName { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]
        public string Category { get; set; } = string.Empty;

        [Range(0, int.MaxValue)]
        public int Quantity { get; set; }
    }
}