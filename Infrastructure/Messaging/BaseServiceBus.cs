using Azure.Messaging.ServiceBus;
using System.Text.Json;

namespace Infrastructure.Messaging;

public interface IBaseServiceBus<TModel>
{
    Task<bool> SendCreatedBookingAsync(TModel bookingModel);
}

public class BaseServiceBus<TModel> : IBaseServiceBus<TModel>
{
    private readonly ServiceBusClient _client;
    private readonly ServiceBusSender _sender;

    public BaseServiceBus(string connectionString, string queueName)
    {
        _client = new ServiceBusClient(connectionString);
        _sender = _client.CreateSender(queueName);
    }

    public async Task<bool> SendCreatedBookingAsync(TModel bookingModel)
    {
        try
        {
            var message = new ServiceBusMessage(JsonSerializer.Serialize(bookingModel));
            await _sender.SendMessageAsync(message);
            return true;
        }
        catch
        {
            return false;
        }
    }
}
