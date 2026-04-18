using System.ComponentModel.DataAnnotations;

namespace InventoryManagement.API.Models.DTOs
{
    public class UpdateOrderDto
    {
        [Required]
        public string Status { get; set; } = default!;

        [Required]
        public decimal TotalAmount { get; set; }
    }
}