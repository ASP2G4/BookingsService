using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Data.Entities;

public class BookingEntity
{
    [Key]
    public int Id { get; set; }
    public int Tickets { get; set; }
    public DateTime CreatedAt { get; private set; }
    public Guid EventId { get; set; }
    public Guid UserId { get; set; }
    public bool Invoiced { get; set; }
    public bool Paid { get; set; }
    public bool Cancelled { get; set; }

    public BookingEntity()
    {
        CreatedAt = DateTime.Now;
    }
}
