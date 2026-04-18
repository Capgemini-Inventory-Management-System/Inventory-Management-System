using System.ComponentModel.DataAnnotations;

namespace InventoryManagement.API.Models.DTOs
{
    public class CreateOrderDto
    {
        [Required]
        public int CustomerId { get; set; }

        [Required]
        public string Status { get; set; } = default!;

        [Required]
        public decimal TotalAmount { get; set; }

        // Perhaps list of OrderItems, but for simplicity, assume separate
    }
}