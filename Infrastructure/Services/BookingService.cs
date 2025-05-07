using Infrastructure.Data.Entities;
using Infrastructure.Mappers;
using Infrastructure.Models;
using Infrastructure.Repositories;

namespace Infrastructure.Services;

public class BookingService(BookingRepo bookingRepo)
{
    private readonly BookingRepo _bookingRepo = bookingRepo;

    public async Task<bool> CreateBookingAsync(AddBookingForm formData)
    {
        if (formData == null)
            return false;

        var booking = BookingMapper.ToEntity(formData);

        bool result = await _bookingRepo.AddAsync(booking);
        return result;
    }

    public async Task<IEnumerable<BookingEntity>> GetAllBookingsAsync()
    {
        var bookings = await _bookingRepo.GetAllAsync();
        return bookings;
    }

    public async Task<IEnumerable<BookingEntity>?> GetAllBookingsByUserIdAsync(Guid userId)
    {
        if (userId == Guid.Empty)
            return null;

        var bookings = await _bookingRepo.GetAllAsync(x => x.UserId == userId);
        return bookings ?? null;
    }

    public async Task<IEnumerable<BookingEntity>?> GetAllBookingsByEventIdAsync(Guid eventId)
    {
        if (eventId == Guid.Empty)
            return null;

        var bookings = await _bookingRepo.GetAllAsync(x => x.EventId == eventId);
        return bookings ?? null;
    }

    public async Task<BookingEntity?> GetBookingByIdAsync(int bookingId)
    {
        var entity = await _bookingRepo.GetAsync(x => x.Id == bookingId);
        return entity ?? null;
    }

    public async Task<bool> UpdateBookingAsync(UpdateBookingForm bookingFormData)
    {
        if (bookingFormData == null)
            return false;

        var oldBooking = await _bookingRepo.GetAsync(x => x.Id == bookingFormData.Id);
        if (oldBooking == null)
            return false;

        var updatedBooking = BookingMapper.ToEntity(bookingFormData, oldBooking);

        var result = await _bookingRepo.UpdateAsync(updatedBooking);
        return result;
    }
}
