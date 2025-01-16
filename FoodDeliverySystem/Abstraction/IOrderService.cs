using FoodDeliverySystem.Models;

namespace FoodDeliverySystem.Abstraction
{
    public interface IOrderService
    {
        Task<Order> PlaceOrderAsync(int customerId, List<OrderItem> orderItems);
        Task<List<Order>> GetOrdersByCustomerAsync(int customerId);
        Task<Order> GetOrderByIdAsync(int orderId);
        Task<Order> UpdateOrderAsync(int orderId, List<OrderItem> updatedOrderItems);
        Task<bool> CancelOrderAsync(int orderId);
    }
}
