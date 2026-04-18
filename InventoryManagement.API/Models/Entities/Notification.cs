using System.ComponentModel.DataAnnotations;

namespace InventoryManagement.API.Models.Entities
{
    public class Notification
    {
        [Key]
        public int NotificationId { get; set; }

        [Required]
        public string UserId { get; set; } = default!;

        [Required]
        [MaxLength(500)]
        public string Message { get; set; } = default!;

        [Required]
        [MaxLength(200)]
        public string ProductName { get; set; } = default!;

        [Required]
        public int Quantity { get; set; }

        [Required]
        public DateTime DateCreated { get; set; } = DateTime.UtcNow;

        // Navigation property
        public User? User { get; set; }
    }
}