namespace Infrastructure.Models;

public class CreatedBookingDto
{
    public int Id { get; set; }
    public int Tickets { get; set; }
    public Guid EventId { get; set; }
    public Guid UserId { get; set; }
}
