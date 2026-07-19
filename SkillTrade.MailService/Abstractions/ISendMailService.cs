using SkillTrade.Core.Models;

namespace SkillTrade.MailService.Abstractions
{
    public interface ISendMailService
    {
        Task<bool> SendEmailAsync(MailContent content, CancellationToken token);
    }
}