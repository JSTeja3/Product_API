using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Product_API.Models
{
    public class Product : BaseEntity
    {
        [Required]
        public string ProductId { get; set; } = string.Empty;

        [Required]
        public string ProductName{ get; set; } = string.Empty;

        [Required]
        public string Category{ get; set; } = string.Empty;

        [Range(0, int.MaxValue)]
        public int Quantity{ get; set; }

        public int Version{ get; set; } = 1;

        public ProductState State{ get; set; } = ProductState.Draft;
    }
}