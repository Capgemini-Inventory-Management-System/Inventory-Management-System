using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace InventoryManagement.API.Models
{
    public class Customer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CustomerId { get; set; }

        [Required]
        [MaxLength(150)]
        public string CustomerName { get; set; } = default!;

        [Required]
        [Phone]
        [MaxLength(20)]
        public string MobileNumber { get; set; } = default!;

        [Required]
        [EmailAddress]
        [MaxLength(200)]
        public string Email { get; set; } = default!;

        public List<Order>? Orders { get; set; }
    }
}