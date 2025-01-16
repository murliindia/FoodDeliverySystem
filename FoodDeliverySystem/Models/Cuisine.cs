namespace FoodDeliverySystem.Models
{
    public class Cuisine
    {
        public int CuisineId { get; set; }
        public string Name { get; set; }
        public ICollection<Dish> Dishes { get; set; }
    }
}
