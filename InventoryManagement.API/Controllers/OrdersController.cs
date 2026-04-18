using InventoryManagement.API.Models.DTOs;
using InventoryManagement.API.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderDto>>> GetAllOrders()
        {
            var orders = await _orderService.GetAllOrdersAsync();
            return Ok(orders);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OrderDto>> GetOrderById(int id)
        {
            var order = await _orderService.GetOrderByIdAsync(id);

            if (order == null)
                return NotFound(new { message = "Order not found" });

            return Ok(order);
        }

        [HttpPost]
        public async Task<ActionResult<OrderDto>> CreateOrder(CreateOrderDto orderDto)
        {
            var createdOrder = await _orderService.CreateOrderAsync(orderDto);
            return CreatedAtAction(nameof(GetOrderById), new { id = createdOrder.OrderId }, createdOrder);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<OrderDto>> UpdateOrder(int id, UpdateOrderDto orderDto)
        {
            var updatedOrder = await _orderService.UpdateOrderAsync(id, orderDto);

            if (updatedOrder == null)
                return NotFound(new { message = "Order not found" });

            return Ok(updatedOrder);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            var deleted = await _orderService.DeleteOrderAsync(id);

            if (!deleted)
                return NotFound(new { message = "Order not found" });

            return Ok(new { message = "Order deleted successfully" });
        }
    }
}