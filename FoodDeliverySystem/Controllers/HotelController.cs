using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using FoodDeliverySystem.Services;
using FoodDeliverySystem.Models;
using FoodDeliverySystem.Abstraction;

namespace FoodDeliverySystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelsController : ControllerBase
    {
        private readonly IHotelService _hotelService;

        public HotelsController(IHotelService hotelService)
        {
            _hotelService = hotelService;
        }

        // GET: api/hotels
        [HttpGet]
        public async Task<IActionResult> GetHotels()
        {
            var hotels = await _hotelService.GetAllHotelsAsync();
            return Ok(hotels);
        }

        // GET: api/hotels/{hotelId}
        [HttpGet("{hotelId}")]
        public async Task<IActionResult> GetHotelById(int hotelId)
        {
            var hotel = await _hotelService.GetHotelByIdAsync(hotelId);
            if (hotel == null)
            {
                return NotFound(); // Hotel not found
            }
            return Ok(hotel);
        }

        // POST: api/hotels
        [HttpPost]
        public async Task<IActionResult> AddHotel([FromBody] Hotel hotel)
        {
            if (hotel == null)
            {
                return BadRequest("Hotel data is required.");
            }

            var createdHotel = await _hotelService.AddHotelAsync(hotel);
            return CreatedAtAction(nameof(GetHotelById), new { hotelId = createdHotel.HotelId }, createdHotel);
        }

        // PUT: api/hotels/{hotelId}
        [HttpPut("{hotelId}")]
        public async Task<IActionResult> UpdateHotel(int hotelId, [FromBody] Hotel hotel)
        {
            if (hotel == null)
            {
                return BadRequest("Hotel data is required.");
            }

            var updatedHotel = await _hotelService.UpdateHotelAsync(hotelId, hotel);
            if (updatedHotel == null)
            {
                return NotFound(); // Hotel not found
            }

            return Ok(updatedHotel);
        }

        // DELETE: api/hotels/{hotelId}
        [HttpDelete("{hotelId}")]
        public async Task<IActionResult> DeleteHotel(int hotelId)
        {
            var isDeleted = await _hotelService.DeleteHotelAsync(hotelId);
            if (!isDeleted)
            {
                return NotFound(); // Hotel not found
            }

            return NoContent(); // Successfully deleted
        }

        // GET: api/hotels/{hotelId}/dishes
        [HttpGet("{hotelId}/dishes")]
        public async Task<IActionResult> GetDishesByHotel(int hotelId)
        {
            var dishes = await _hotelService.GetDishesByHotelAsync(hotelId);
            if (dishes == null || !dishes.Any())
            {
                return NotFound(); // No dishes found for this hotel
            }
            return Ok(dishes);
        }

        // POST: api/hotels/{hotelId}/dishes
        [HttpPost("{hotelId}/dishes")]
        public async Task<IActionResult> AddDishToHotel(int hotelId, [FromBody] Dish dish)
        {
            if (dish == null)
            {
                return BadRequest("Dish data is required.");
            }

            var addedDish = await _hotelService.AddDishToHotelAsync(hotelId, dish);
            if (addedDish == null)
            {
                return NotFound(); // Hotel not found
            }

            return CreatedAtAction(nameof(GetDishesByHotel), new { hotelId = hotelId }, addedDish);
        }

        // PUT: api/hotels/{hotelId}/dishes/{dishId}
        [HttpPut("{hotelId}/dishes/{dishId}")]
        public async Task<IActionResult> UpdateDish(int hotelId, int dishId, [FromBody] Dish dish)
        {
            if (dish == null)
            {
                return BadRequest("Dish data is required.");
            }

            var updatedDish = await _hotelService.UpdateDishAsync(hotelId, dishId, dish);
            if (updatedDish == null)
            {
                return NotFound(); // Dish or hotel not found
            }

            return Ok(updatedDish);
        }

        // PUT: api/hotels/{hotelId}/dishes/{dishId}/availability
        [HttpPut("{hotelId}/dishes/{dishId}/availability")]
        public async Task<IActionResult> SetDishAvailability(int hotelId, int dishId, [FromQuery] bool isAvailable)
        {
            var updatedDish = await _hotelService.SetDishAvailabilityAsync(hotelId, dishId, isAvailable);
            if (updatedDish == null)
            {
                return NotFound(); // Dish or hotel not found
            }

            return Ok(updatedDish);
        }
    }

}
