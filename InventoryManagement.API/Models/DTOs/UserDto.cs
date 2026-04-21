namespace InventoryManagement.API.Models.DTOs
{
    public class UserDto
    {
        public string UserId { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string UserName { get; set; } = null!;
        public string MobileNumber { get; set; } = null!;
        public string UserRole { get; set; } = null!;
    }
}