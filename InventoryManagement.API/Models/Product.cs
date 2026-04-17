using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InventoryManagement.API.Models
{
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProductId { get; set; }

        [Required]
        [MaxLength(200)]
        public string Name { get; set; } = default!;

        [MaxLength(1000)]
        public string? Description { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        [Required]
        public int Quantity { get; set; }

        public List<Order>? Orders { get; set; }
        public List<OrderItem>? OrderItems { get; set; }
    }
}