using SkillTrade.MailService.Handlers;
using SkillTrade.MessageBroker.RabbitMQ.Models;
using SkillTrade.MessageBroker.RabbitMQ.Services;

namespace SkillTrade.MailService
{
    public class Worker(ILogger<Worker> logger,
        IServiceProvider serviceProvider,
        ProviderRabbitMQService rabbitMQ) : BackgroundService
    {
        
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            try
            {
                await rabbitMQ.InitializeQueuesAsync();
                using var scope = serviceProvider.CreateScope();
                var handler = scope.ServiceProvider.GetRequiredService<EmailMessageHandler>();
                await rabbitMQ.ReceiveMessageAsync(RoutingKeys.Emails, async (messageJson) =>
                {
                    try
                    {
                        return await handler.HandleMessageAsync(messageJson);
                    }
                    catch
                    {
                        return "error";
                    }
                });
                while (!stoppingToken.IsCancellationRequested)
                {
                    if (logger.IsEnabled(LogLevel.Information))
                    {
                        logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                    }
                    await Task.Delay(1000, stoppingToken);
                }
            }
            catch
            {
                Console.WriteLine("Error execute");
            }
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            rabbitMQ.Dispose();
            return base.StopAsync(cancellationToken);
        }
    }
}
