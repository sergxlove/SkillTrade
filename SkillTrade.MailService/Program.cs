using SkillTrade.MailService.Abstractions;
using SkillTrade.MailService.Handlers;
using SkillTrade.MailService.Services;
using SkillTrade.MessageBroker.RabbitMQ.Services;

namespace SkillTrade.MailService
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = Host.CreateApplicationBuilder(args);
            builder.Services.AddHostedService<Worker>();
            builder.Services.AddSingleton<ProviderRabbitMQService>(sp =>
            {
                var rabbitMQ = ProviderRabbitMQService.CreateAsync().GetAwaiter().GetResult();
                return rabbitMQ;
            });
            builder.Services.AddScoped<ISendMailService, SendMailService>();
            builder.Services.AddScoped<EmailMessageHandler>();

            var host = builder.Build();

            using (var scope = host.Services.CreateScope())
            {
                var rabbitMQ = scope.ServiceProvider.GetRequiredService<ProviderRabbitMQService>();
                await rabbitMQ.InitializeQueuesAsync();
                Console.WriteLine("RabbitMQ queues initialized");
            }

            host.Run();
        }
    }
}
