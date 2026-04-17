using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InventoryManagement.API.Models
{
    public class Order
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OrderID { get; set; }

        [Required]
        public int CustomerID { get; set; }

        public Customer? Customer { get; set; }

        [Required]
        public DateTime OrderDate { get; set; } = DateTime.UtcNow;

        [Required]
        [MaxLength(50)]
        public string Status { get; set; } = default!;

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalAmount { get; set; }

        public List<OrderItem>? OrderItems { get; set; }
        public List<Product>? Products { get; set; }
    }
}