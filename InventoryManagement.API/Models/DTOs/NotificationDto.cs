namespace InventoryManagement.API.Models.DTOs
{
    public class NotificationDto
    {
        public int NotificationId { get; set; }
        public string UserId { get; set; } = default!;
        public string Message { get; set; } = default!;
        public string ProductName { get; set; } = default!;
        public int Quantity { get; set; }
        public DateTime DateCreated { get; set; }
    }
}