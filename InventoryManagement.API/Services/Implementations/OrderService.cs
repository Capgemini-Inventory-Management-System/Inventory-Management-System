using InventoryManagement.API.Data;
using InventoryManagement.API.Models.Entities;
using InventoryManagement.API.Models.DTOs;
using InventoryManagement.API.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagement.API.Services.Implementations
{
    public class OrderService : IOrderService
    {
        private readonly ApplicationDbContext _context;

        public OrderService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<OrderDto>> GetAllOrdersAsync()
        {
            var orders = await _context.Orders
                .Include(o => o.Customer)
                .Include(o => o.OrderItems)
                .ToListAsync();

            return orders.Select(o => new OrderDto
            {
                OrderId = o.OrderId,
                CustomerId = o.CustomerId,
                OrderDate = o.OrderDate,
                Status = o.Status,
                TotalAmount = o.TotalAmount
            });
        }

        public async Task<OrderDto?> GetOrderByIdAsync(int id)
        {
            var order = await _context.Orders
                .Include(o => o.Customer)
                .Include(o => o.OrderItems)
                .FirstOrDefaultAsync(o => o.OrderId == id);

            if (order == null) return null;

            return new OrderDto
            {
                OrderId = order.OrderId,
                CustomerId = order.CustomerId,
                OrderDate = order.OrderDate,
                Status = order.Status,
                TotalAmount = order.TotalAmount
            };
        }

        public async Task<OrderDto> CreateOrderAsync(CreateOrderDto orderDto)
        {
            try
            {
                var order = new Order
                {
                    CustomerId = orderDto.CustomerId,
                    OrderDate = DateTime.UtcNow,
                    Status = orderDto.Status,
                    TotalAmount = orderDto.TotalAmount
                };

                _context.Orders.Add(order);
                await _context.SaveChangesAsync();

                return new OrderDto
                {
                    OrderId = order.OrderId,
                    CustomerId = order.CustomerId,
                    OrderDate = order.OrderDate,
                    Status = order.Status,
                    TotalAmount = order.TotalAmount
                };
            }
            catch (Exception ex)
            {
                throw new Exception("Error creating order: " + ex.Message);
            }
        }

        public async Task<OrderDto?> UpdateOrderAsync(int id, UpdateOrderDto orderDto)
        {
            var existingOrder = await _context.Orders
                .FirstOrDefaultAsync(o => o.OrderId == id);

            if (existingOrder == null)
                return null;

            existingOrder.Status = orderDto.Status;
            existingOrder.TotalAmount = orderDto.TotalAmount;

            await _context.SaveChangesAsync();

            return new OrderDto
            {
                OrderId = existingOrder.OrderId,
                CustomerId = existingOrder.CustomerId,
                OrderDate = existingOrder.OrderDate,
                Status = existingOrder.Status,
                TotalAmount = existingOrder.TotalAmount
            };
        }

        public async Task<bool> DeleteOrderAsync(int id)
        {
            var order = await _context.Orders.FindAsync(id);

            if (order == null)
                return false;

            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}