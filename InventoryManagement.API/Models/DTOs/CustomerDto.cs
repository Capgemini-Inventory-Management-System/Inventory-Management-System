namespace InventoryManagement.API.Models.DTOs
{
    public class CustomerDto
    {
        public int CustomerId { get; set; }
        public string CustomerName { get; set; } = string.Empty;
        public string MobileNumber { get; set; } = string.Empty;
        public string? Email { get; set; }
    }
}
