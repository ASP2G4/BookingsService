namespace Infrastructure.Models;

public class CreatedBookingDto
{
    public int Id { get; set; }
    public int Tickets { get; set; }
    public string EventId { get; set; } = null!;
    public string UserId { get; set; } = null!;
}
