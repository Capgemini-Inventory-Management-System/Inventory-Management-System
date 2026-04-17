using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace InventoryManagement.API.Models
{
    public class User
    {
    
            [Key]
            public long UserId { get; set; }

            [Required]
            [EmailAddress]
            public string Email { get; set; }

            [Required]
            public  string UserName { get; set; }

            [Required]
            [Phone]
            public string MobileNumber { get; set; }

            [Required]
            public string UserRole { get; set; }

            // Navigation property
            public ICollection<Notification>? Notifications { get; set; }
        }
    }
