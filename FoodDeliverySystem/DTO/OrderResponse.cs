namespace FoodDeliverySystem.DTO
{
    public class OrderResponse
    {
        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        public List<OrderItemResponse> OrderItems { get; set; }
    }
}
