using Infrastructure.Models;

namespace Infrastructure.Messaging;

public class EmailServiceBus(string connectionString, string queueName) : BaseServiceBus<CreatedBookingDto>(connectionString, queueName)
{
}
