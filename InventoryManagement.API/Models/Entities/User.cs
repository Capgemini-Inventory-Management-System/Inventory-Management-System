using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace InventoryManagement.API.Models.Entities
{
    public class User
    {
        [Key]
        public string UserId { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        [Phone]
        public string MobileNumber { get; set; }

        [Required]
        public string UserRole { get; set; }

        // Navigation property
        public virtual ICollection<Notification> Notifications { get; set; } = new List<Notification>();
    }
}