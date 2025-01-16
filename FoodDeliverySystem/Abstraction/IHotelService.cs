namespace FoodDeliverySystem.Abstraction
{
    using System.Collections.Generic;
    using FoodDeliverySystem.Models;

    public interface IHotelService
    {
        // Get all hotels
        Task<IEnumerable<Hotel>> GetAllHotelsAsync();

        // Get a hotel by its ID
        Task<Hotel> GetHotelByIdAsync(int hotelId);

        // Add a new hotel
        Task<Hotel> AddHotelAsync(Hotel hotel);

        // Update an existing hotel
        Task<Hotel> UpdateHotelAsync(int hotelId, Hotel hotel);

        // Delete a hotel by its ID
        Task<bool> DeleteHotelAsync(int hotelId);

        // Get all dishes for a specific hotel
        Task<IEnumerable<Dish>> GetDishesByHotelAsync(int hotelId);

        // Add a new dish to a hotel menu
        Task<Dish> AddDishToHotelAsync(int hotelId, Dish dish);

        // Update a dish in a hotel's menu
        Task<Dish> UpdateDishAsync(int hotelId, int dishId, Dish dish);

        // Mark a dish as available or unavailable
        Task<Dish> SetDishAvailabilityAsync(int hotelId, int dishId, bool isAvailable);
    }

}
