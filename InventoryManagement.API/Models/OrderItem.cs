using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InventoryManagement.API.Models
{
    public class OrderItem
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OrderItemID { get; set; }

        [Required]
        public int OrderID { get; set; }

        [Required]
        public int ProductID { get; set; }

        [Required]
        public int Quantity { get; set; }

        public Product? Product { get; set; }
        public Order? Order { get; set; }
    }
}