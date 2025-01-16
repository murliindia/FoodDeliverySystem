namespace FoodDeliverySystem.Models
{
    using System.ComponentModel.DataAnnotations;
    public class Dish
    {

        public int DishId { get; set; }
        public int HotelId { get; set; }
        public int CuisineId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public bool IsAvailable { get; set; }

        public Hotel Hotel { get; set; }
        public Cuisine Cuisine { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; }

    }
}
