using FoodDeliverySystem.Abstraction;
using FoodDeliverySystem.Data;
using FoodDeliverySystem.Models;
using Microsoft.EntityFrameworkCore;

namespace FoodDeliverySystem.Services
{
    public class OrderService : IOrderService
    {
        private readonly ApplicationDbContext _context;

        public OrderService(ApplicationDbContext context)
        {
            _context = context;
        }

        // Method to place an order
        public async Task<Order> PlaceOrderAsync(int customerId, List<OrderItem> orderItems)
        {
            // Check if all dishes are available
            foreach (var item in orderItems)
            {
                var dish = await _context.Dishes.FindAsync(item.DishId);
                if (dish == null || !dish.IsAvailable)
                {
                    throw new InvalidOperationException($"Dish {item.DishId} is either unavailable or does not exist.");
                }
            }

            // Calculate total order amount
            decimal totalAmount = 0;
            foreach (var item in orderItems)
            {
                var dish = await _context.Dishes.FindAsync(item.DishId);
                totalAmount += dish.Price * item.Quantity;
            }

            // Create a new order
            var order = new Order
            {
                CustomerId = customerId,
                OrderDate = DateTime.UtcNow,
                TotalAmount = totalAmount,
                OrderItems = orderItems
            };

            // Add the order and its items to the database
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            return order;
        }

        // Method to retrieve all orders for a customer
        public async Task<List<Order>> GetOrdersByCustomerAsync(int customerId)
        {
            return await _context.Orders
                .Where(o => o.CustomerId == customerId)
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Dish)
                .ToListAsync();
        }

        // Method to retrieve a specific order by its ID
        public async Task<Order> GetOrderByIdAsync(int orderId)
        {
            return await _context.Orders
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Dish)
                .FirstOrDefaultAsync(o => o.OrderId == orderId);
        }

        // Method to update an order (if needed, like modifying quantity or adding/removing items)
        public async Task<Order> UpdateOrderAsync(int orderId, List<OrderItem> updatedOrderItems)
        {
            var order = await _context.Orders.Include(o => o.OrderItems).FirstOrDefaultAsync(o => o.OrderId == orderId);
            if (order == null)
            {
                throw new KeyNotFoundException($"Order with ID {orderId} not found.");
            }

            // Remove existing order items
            _context.OrderItems.RemoveRange(order.OrderItems);

            // Check if all dishes are available before updating
            foreach (var item in updatedOrderItems)
            {
                var dish = await _context.Dishes.FindAsync(item.DishId);
                if (dish == null || !dish.IsAvailable)
                {
                    throw new InvalidOperationException($"Dish {item.DishId} is either unavailable or does not exist.");
                }
            }

            // Recalculate total amount
            decimal totalAmount = 0;
            foreach (var item in updatedOrderItems)
            {
                var dish = await _context.Dishes.FindAsync(item.DishId);
                totalAmount += dish.Price * item.Quantity;
            }

            // Update the order details
            order.OrderItems = updatedOrderItems;
            order.TotalAmount = totalAmount;

            await _context.SaveChangesAsync();
            return order;
        }

        // Method to cancel an order (mark as canceled or delete)
        public async Task<bool> CancelOrderAsync(int orderId)
        {
            var order = await _context.Orders.FindAsync(orderId);
            if (order == null)
            {
                throw new KeyNotFoundException($"Order with ID {orderId} not found.");
            }

            // Optionally, you could set an "OrderStatus" property to 'Canceled' instead of deleting the order.
            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();
            return true;
        }
    }

}
