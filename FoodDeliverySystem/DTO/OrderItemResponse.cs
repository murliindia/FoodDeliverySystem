namespace FoodDeliverySystem.DTO
{
    public class OrderItemResponse
    {
        public int DishId { get; set; }
        public string DishName { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
