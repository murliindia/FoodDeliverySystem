namespace FoodDeliverySystem.DTO
{
    public class OrderRequest
    {
        public int CustomerId { get; set; }
        public List<OrderItemRequest> OrderItems { get; set; }
    }
}
