namespace Infrastructure.Models;

public class AddBookingForm
{
    public int Tickets { get; set; }
    public string EventId { get; set; } = null!;
    public string UserId { get; set; } = null!;
    public bool Paid { get; set; }
}


//    public int Id { get; set; }
//    public int Tickets { get; set; }
//    public DateTime CreatedAt { get; set; }
//    public Guid EventId { get; set; }
//    public Guid UserId { get; set; }
//    public bool Invoiced { get; set; }
//    public bool Paid { get; set; }
//    public bool Cancelled { get; set; }