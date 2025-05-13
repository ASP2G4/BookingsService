using Azure.Messaging.ServiceBus;

namespace Infrastructure.Messaging
{
    public class ServiceBus
    {
        private readonly ServiceBusClient _client;
        private readonly ServiceBusSender _sender;

        public ServiceBus(string connectionString, string queueName)
        {
            _client = new ServiceBusClient(connectionString);
            _sender = _client.CreateSender(queueName);
        }

        public async Task<bool> SendCreatedBookingAsync(string input)
        {
            try
            {
                var message = new ServiceBusMessage(input);
                await _sender.SendMessageAsync(message);
                await _sender.DisposeAsync();
                await _client.DisposeAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
