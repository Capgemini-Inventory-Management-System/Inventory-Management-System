using System.ComponentModel.DataAnnotations;

namespace InventoryManagement.API.Models.DTOs
{
    public class UpdateCustomer
    {
        [Required, MaxLength(150)]
        public string CustomerName { get; set; } = string.Empty;

        [Required, Phone, MaxLength(20)]
        public string MobileNumber { get; set; } = string.Empty;

        [Required, EmailAddress, MaxLength(200)]
        public string Email { get; set; } = string.Empty;
    }
}
