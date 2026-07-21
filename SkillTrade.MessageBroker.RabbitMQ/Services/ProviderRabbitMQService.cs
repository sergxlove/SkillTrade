using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using SkillTrade.MessageBroker.RabbitMQ.Abstractions;
using SkillTrade.MessageBroker.RabbitMQ.Models;
using System.Text;
using System.Text.Json;

namespace SkillTrade.MessageBroker.RabbitMQ.Services
{
    public class ProviderRabbitMQService : IDisposable, IProviderRabbitMQService
    {
        private IConnection _connection;
        private IChannel _channel;
        private string _exchangeName { get; set; } = "default";
        private readonly Dictionary<RoutingKeys, string> _queueMappings = new()
        {
            [RoutingKeys.Emails] = "emails_queue",
        };
        private JsonSerializerOptions _jsonOptions = new JsonSerializerOptions()
        {
            WriteIndented = false
        };
        private bool _isDisposed;

        private ProviderRabbitMQService(IConnection connection, IChannel channel)
        {
            _connection = connection;
            _channel = channel;
        }

        public static async Task<ProviderRabbitMQService> CreateAsync()
        {
            var factory = new ConnectionFactory
            {
                HostName = "localhost",  
                Port = 5672,
                UserName = "admin",
                Password = "admin123"
            };
            var connection = await factory.CreateConnectionAsync();
            var channel = await connection.CreateChannelAsync();
            await channel.ExchangeDeclareAsync(
                exchange: "default",
                type: ExchangeType.Direct,
                durable: true,
                autoDelete: false
            );
            return new ProviderRabbitMQService(connection, channel);
        }

        public async Task InitializeQueuesAsync()
        {
            foreach (var (routingKey, queueName) in _queueMappings)
            {
                string key = RoutingKeysToString(routingKey);

                await _channel.QueueDeclareAsync(
                    queue: queueName,
                    durable: true,
                    exclusive: false,
                    autoDelete: false
                );
                await _channel.QueueBindAsync(
                    queue: queueName,
                    exchange: _exchangeName,
                    routingKey: key
                );
            }
        }

        public string GetQueueName(RoutingKeys routingKey)
        {
            return _queueMappings[routingKey];
        }

        private string RoutingKeysToString(RoutingKeys keys)
        {
            switch (keys)
            {
                case RoutingKeys.Emails:
                    return "emails";
                default:
                    return "none";
            }
        }

        public async Task<bool> SendMessageAsync<T>(T message, RoutingKeys routingKey)
        {
            try
            {
                string key = RoutingKeysToString(routingKey);
                string json = JsonSerializer.Serialize(message, _jsonOptions);
                byte[] body = Encoding.UTF8.GetBytes(json);
                if (!_channel.IsOpen)
                {
                    return false;
                }
                await _channel.BasicPublishAsync(exchange: _exchangeName, routingKey: key, body: body);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> ReceiveMessageAsync(RoutingKeys routingKey, Func<string, Task<string>> messageHandler)
        {
            try
            {
                string queueName = GetQueueName(routingKey);
                await _channel.QueueDeclareAsync(
                    queue: queueName,
                    durable: true,
                    exclusive: false,
                    autoDelete: false
                );
                string key = RoutingKeysToString(routingKey);
                await _channel.QueueBindAsync(
                    queue: queueName,
                    exchange: _exchangeName,
                    routingKey: key
                );
                var consumer = new AsyncEventingBasicConsumer(_channel);
                consumer.ReceivedAsync += async (model, ea) =>
                {
                    try
                    {
                        var body = ea.Body.ToArray();
                        var message = Encoding.UTF8.GetString(body);
                        var receivedRoutingKey = ea.RoutingKey;
                        if (messageHandler != null)
                        {
                            await messageHandler(message);
                        }
                        await _channel.BasicAckAsync(ea.DeliveryTag, false);
                    }
                    catch
                    {
                        await _channel.BasicNackAsync(ea.DeliveryTag, false, true);
                    }
                };
                await _channel.BasicConsumeAsync(
                    queue: queueName,
                    autoAck: false,
                    consumer: consumer
                );
                return true;
            }
            catch
            {
                return false;
            }
        }

        public void Dispose()
        {
            if (_isDisposed) return;

            _channel?.CloseAsync();
            _connection?.CloseAsync();
            _channel?.Dispose();
            _connection?.Dispose();
            _isDisposed = true;
        }
    }
}
