using SkillTrade.MessageBroker.RabbitMQ.Models;
using SkillTrade.MessageBroker.RabbitMQ.Services;

namespace SkillTrade.MessageBroker.RabbitMQ.Abstractions
{
    public interface IProviderRabbitMQService
    {
        static abstract Task<ProviderRabbitMQService> CreateAsync();
        void Dispose();
        string GetQueueName(RoutingKeys routingKey);
        Task InitializeQueuesAsync();
        Task<bool> ReceiveMessageAsync(RoutingKeys routingKey, Func<string, Task<string>> messageHandler);
        Task<bool> SendMessageAsync<T>(T message, RoutingKeys routingKey);
    }
}