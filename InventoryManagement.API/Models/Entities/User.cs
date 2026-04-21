using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace InventoryManagement.API.Models.Entities
{
    public class User
    {
        [Key]
        public string UserId { get; set; } = null!;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = null!;

        [Required]
        public string Password { get; set; } = null!;

        [Required]
        public string UserName { get; set; } = null!;

        [Required]
        [Phone]
        public string MobileNumber { get; set; } = null!;

        [Required]
        public string UserRole { get; set; } = null!;

        // Navigation property
        public virtual ICollection<Notification> Notifications { get; set; } = new List<Notification>();
    }
}