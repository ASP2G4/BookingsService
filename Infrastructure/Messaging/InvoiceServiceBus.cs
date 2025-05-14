using Infrastructure.Models;

namespace Infrastructure.Messaging;

public class InvoiceServiceBus(string connectionString, string queueName) : BaseServiceBus<CreatedBookingDto>(connectionString, queueName)
{
}
