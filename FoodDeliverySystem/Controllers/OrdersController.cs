using FoodDeliverySystem.Abstraction;
using FoodDeliverySystem.DTO;
using FoodDeliverySystem.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FoodDeliverySystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;

        // Inject IOrderService via constructor
        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost]
        public async Task<IActionResult> PlaceOrder([FromBody] OrderRequest orderRequest)
        {
            var orderItems = orderRequest.OrderItems.Select(item => new OrderItem
            {
                DishId = item.DishId,
                Quantity = item.Quantity
            }).ToList();

            var order = await _orderService.PlaceOrderAsync(orderRequest.CustomerId, orderItems);
            return CreatedAtAction(nameof(GetOrder), new { orderId = order.OrderId }, order);
        }

        [HttpGet("{customerId}")]
        public async Task<ActionResult<List<Order>>> GetOrdersByCustomer(int customerId)
        {
            var orders = await _orderService.GetOrdersByCustomerAsync(customerId);
            return Ok(orders);
        }

        [HttpGet("{orderId}")]
        public async Task<ActionResult<Order>> GetOrder(int orderId)
        {
            var order = await _orderService.GetOrderByIdAsync(orderId);
            if (order == null)
            {
                return NotFound();
            }
            return Ok(order);
        }

        [HttpPut("{orderId}")]
        public async Task<IActionResult> UpdateOrder(int orderId, [FromBody] List<OrderItem> updatedOrderItems)
        {
            var order = await _orderService.UpdateOrderAsync(orderId, updatedOrderItems);
            return NoContent();
        }

        [HttpDelete("{orderId}")]
        public async Task<IActionResult> CancelOrder(int orderId)
        {
            var success = await _orderService.CancelOrderAsync(orderId);
            if (success)
            {
                return NoContent();
            }
            return NotFound();
        }
    }

}
