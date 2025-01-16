namespace FoodDeliverySystem.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using FoodDeliverySystem.Abstraction;
    using FoodDeliverySystem.Data;
    using FoodDeliverySystem.Models;
    using Microsoft.EntityFrameworkCore;

    public class HotelService : IHotelService
    {
        private readonly ApplicationDbContext _context;

        public HotelService(ApplicationDbContext context)
        {
            _context = context;
        }

        // Get all hotels
        public async Task<IEnumerable<Hotel>> GetAllHotelsAsync()
        {
            return await _context.Hotels
                .Include(h => h.Dishes)  // Include dishes for each hotel
                .ToListAsync();
        }

        // Get a hotel by its ID
        public async Task<Hotel> GetHotelByIdAsync(int hotelId)
        {
            return await _context.Hotels
                .Include(h => h.Dishes)  // Include dishes for the hotel
                .FirstOrDefaultAsync(h => h.HotelId == hotelId);
        }

        // Add a new hotel
        public async Task<Hotel> AddHotelAsync(Hotel hotel)
        {
            _context.Hotels.Add(hotel);
            await _context.SaveChangesAsync();
            return hotel;
        }

        // Update an existing hotel
        public async Task<Hotel> UpdateHotelAsync(int hotelId, Hotel hotel)
        {
            var existingHotel = await _context.Hotels
                .FirstOrDefaultAsync(h => h.HotelId == hotelId);

            if (existingHotel == null)
            {
                return null; // Hotel not found
            }

            existingHotel.Name = hotel.Name;
            existingHotel.Address = hotel.Address;

            // Update the hotel in the database
            await _context.SaveChangesAsync();
            return existingHotel;
        }

        // Delete a hotel by its ID
        public async Task<bool> DeleteHotelAsync(int hotelId)
        {
            var hotel = await _context.Hotels.FindAsync(hotelId);
            if (hotel == null)
            {
                return false; // Hotel not found
            }

            _context.Hotels.Remove(hotel);
            await _context.SaveChangesAsync();
            return true;
        }

        // Get all dishes for a specific hotel
        public async Task<IEnumerable<Dish>> GetDishesByHotelAsync(int hotelId)
        {
            return await _context.Dishes
                .Where(d => d.HotelId == hotelId)
                .ToListAsync();
        }

        // Add a new dish to a hotel menu
        public async Task<Dish> AddDishToHotelAsync(int hotelId, Dish dish)
        {
            var hotel = await _context.Hotels.FindAsync(hotelId);
            if (hotel == null)
            {
                return null; // Hotel not found
            }

            dish.HotelId = hotelId;
            _context.Dishes.Add(dish);
            await _context.SaveChangesAsync();
            return dish;
        }

        // Update a dish in a hotel's menu
        public async Task<Dish> UpdateDishAsync(int hotelId, int dishId, Dish dish)
        {
            var existingDish = await _context.Dishes
                .FirstOrDefaultAsync(d => d.DishId == dishId && d.HotelId == hotelId);

            if (existingDish == null)
            {
                return null; // Dish not found
            }

            existingDish.Name = dish.Name;
            existingDish.Description = dish.Description;
            existingDish.Price = dish.Price;

            await _context.SaveChangesAsync();
            return existingDish;
        }

        // Mark a dish as available or unavailable
        public async Task<Dish> SetDishAvailabilityAsync(int hotelId, int dishId, bool isAvailable)
        {
            var dish = await _context.Dishes
                .FirstOrDefaultAsync(d => d.DishId == dishId && d.HotelId == hotelId);

            if (dish == null)
            {
                return null; // Dish not found
            }

            dish.IsAvailable = isAvailable;
            await _context.SaveChangesAsync();
            return dish;
        }
    }


}
