namespace InventoryManagement.API.Models.DTOs
{
    public class OrderDto
    {
        public int OrderId { get; set; }
        public int CustomerId { get; set; }
        public DateTime OrderDate { get; set; }
        public string Status { get; set; } = default!;
        public decimal TotalAmount { get; set; }
        public List<OrderItemDto>? OrderItems { get; set; }
    }
}