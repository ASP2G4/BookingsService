using Infrastructure.Models;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BookingsController(BookingService bookingService) : ControllerBase
{
    private readonly BookingService _bookingService = bookingService;

    [HttpPost]
    public async Task<IActionResult> Create(AddBookingForm bookingFormData)
    {
        if (!ModelState.IsValid)
            return BadRequest(bookingFormData);

        var result = await _bookingService.CreateBookingAsync(bookingFormData);
        return result ? Ok(result) : BadRequest();
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var bookings = await _bookingService.GetAllBookingsAsync();
        return Ok(bookings);
    }

    [HttpGet("{bookingId}")]
    public async Task<IActionResult> Get(int bookingId)
    {
        var booking = await _bookingService.GetBookingByIdAsync(bookingId);
        return booking == null ? NotFound() : Ok(booking);
    }

    [HttpGet("user/{userId}")]
    public async Task<IActionResult> GetAllByUserId(Guid userId)
    {
        var bookings = await _bookingService.GetAllBookingsByUserIdAsync(userId);
        return bookings == null ? NotFound() : Ok(bookings);
    }

    [HttpGet("event/{eventId}")]
    public async Task<IActionResult> GetAllByEventId(Guid eventId)
    {
        var bookings = await _bookingService.GetAllBookingsByEventIdAsync(eventId);
        return bookings == null ? NotFound() : Ok(bookings);
    }

    [HttpPut]
    public async Task<IActionResult> Update(UpdateBookingForm bookingFormData)
    {
        if (!ModelState.IsValid)
            return BadRequest(bookingFormData);

        var result = await _bookingService.UpdateBookingAsync(bookingFormData);
        return result ? Ok(result) : BadRequest();
    }
}
