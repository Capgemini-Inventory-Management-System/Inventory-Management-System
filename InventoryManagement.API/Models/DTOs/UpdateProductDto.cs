using System.ComponentModel.DataAnnotations;

namespace InventoryManagement.API.Models.DTOs
{
    public class UpdateProductDto
    {
        [Required, MaxLength(200)]
        public string Name { get; set; } = string.Empty;

        [MaxLength(1000)]
        public string? Description { get; set; }

        [Required, Range(0.01, double.MaxValue)]
        public decimal Price { get; set; }

        [Required, Range(0, int.MaxValue)]
        public int Quantity { get; set; }
    }
}