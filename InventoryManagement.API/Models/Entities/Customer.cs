using System.ComponentModel.DataAnnotations;

namespace InventaryManagement.Models
{
    public class Customer
    {
        [Key]
        [Required(ErrorMessage = "CustomerId is required"), MaxLength(100)]
        public string CustomerId { get; set; } = string.Empty;
        [Required(ErrorMessage = "Name is required"), MaxLength(20)]
        public string CustomerName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "PhoneNumber is required")]
        [Phone(ErrorMessage = "Invalid Phone Number")]
        public string MobileNumber { get; set; }

        public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}
