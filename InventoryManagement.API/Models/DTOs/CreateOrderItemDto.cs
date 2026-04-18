using System.ComponentModel.DataAnnotations;

namespace InventoryManagement.API.Models.DTOs
{
    public class CreateOrderItemDto
    {
        [Required]
        public int OrderId { get; set; }

        [Required]
        public int ProductId { get; set; }

        [Required]
        public int Quantity { get; set; }
    }
}