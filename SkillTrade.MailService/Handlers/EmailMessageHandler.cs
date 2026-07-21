using SkillTrade.Core.Models;
using SkillTrade.MailService.Abstractions;
using System.Text.Json;

namespace SkillTrade.MailService.Handlers
{
    public class EmailMessageHandler
    {
        private readonly ISendMailService _mailService;
        private JsonSerializerOptions _options = new()
        {
            PropertyNameCaseInsensitive = true
        };
        public EmailMessageHandler(ISendMailService mailService)
        {
            _mailService = mailService;
        }

        public async Task<string> HandleMessageAsync(string messageJson)
        {
            try
            {
                MailContent? mailContent = JsonSerializer.Deserialize<MailContent>(messageJson, _options);
                if (mailContent == null)
                {
                    return "Error deserialize";
                }
                CancellationTokenSource cts = new(1000);
                bool result = await _mailService.SendEmailAsync(mailContent, cts.Token);
                if (result)
                    return string.Empty;
                else
                    return "Error send in service";
            }
            catch
            {
                return "Error send in handler";
            }
        }
    }
}
