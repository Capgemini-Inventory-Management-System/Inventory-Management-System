using System.ComponentModel.DataAnnotations;

namespace InventaryManagement.Models.DTOs
{
    public class UpdateCustomer
    {
        [Required, MaxLength(100)]
        public string CustomerName { get; set; } = string.Empty;

        [Required, MaxLength(15), RegularExpression(@"^\d{10}$", ErrorMessage = "Mobile number must be 10 digits")]
        public string MobileNumber { get; set; } = string.Empty;

        [EmailAddress, MaxLength(100)]
        public string? Email { get; set; }
    }
}
