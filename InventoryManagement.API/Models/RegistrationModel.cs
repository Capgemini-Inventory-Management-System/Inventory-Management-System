using System.ComponentModel.DataAnnotations;

namespace InventoryManagement.API.Models
{
    public class RegistrationModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = default!;

        [Required]
        [MinLength(6)]
        public string Password { get; set; } = default!;

        [Required]
        [Compare("Password")]
        public string ConfirmPassword { get; set; } = default!;

        [Required]
        public string Username { get; set; } = default!;

        [Required]
        [Phone]
        public string MobileNumber { get; set; } = default!;

        [Required]
        public string Role { get; set; } = default!;   // ✅ rename (not UserRole)
    }
}