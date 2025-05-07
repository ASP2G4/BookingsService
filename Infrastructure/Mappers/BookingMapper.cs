using Infrastructure.Data.Entities;
using Infrastructure.Models;

namespace Infrastructure.Mappers;

public static class BookingMapper
{
    public static BookingEntity ToEntity(AddBookingForm formData)
    {
        return formData == null ? null! : new BookingEntity()
        {
            Tickets = formData.Tickets,
            EventId = formData.EventId,
            UserId = formData.UserId,
            Invoiced = false,
            Paid = formData.Paid,
            Cancelled = false
        };
    }

    public static BookingEntity ToEntity(UpdateBookingForm updateFormData, BookingEntity entity)
    {
        if (updateFormData == null)
            return null!;

        entity.Invoiced = updateFormData.Invoiced;
        entity.Paid = updateFormData.Paid;
        entity.Cancelled = updateFormData.Cancelled;

        return entity;
    }

}
