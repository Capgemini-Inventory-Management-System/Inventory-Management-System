using InventoryManagement.API.Models.DTOs;

namespace InventoryManagement.API.Services.Interfaces
{
    public interface IOrderService
    {
        Task<IEnumerable<OrderDto>> GetAllOrdersAsync();
        Task<OrderDto?> GetOrderByIdAsync(int id);
        Task<OrderDto> CreateOrderAsync(CreateOrderDto orderDto);
        Task<OrderDto?> UpdateOrderAsync(int id, UpdateOrderDto orderDto);
        Task<bool> DeleteOrderAsync(int id);
    }
}