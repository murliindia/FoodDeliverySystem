namespace FoodDeliverySystem.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Menu
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Cuisine { get; set; }

        public int HotelId { get; set; }
        public Hotel Hotel { get; set; }

        public ICollection<Dish> Dishes { get; set; }
    }
}
