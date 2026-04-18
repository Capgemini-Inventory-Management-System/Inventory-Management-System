using System.ComponentModel.DataAnnotations;

namespace InventoryManagement.API.Models.DTOs
{
    public class UpdateOrderItemDto
    {
        [Required]
        public int Quantity { get; set; }
    }
}