using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using SkillTrade.Core.Models;
using SkillTrade.MailService.Abstractions;
using SkillTrade.MailService.Infrastructures;

namespace SkillTrade.MailService.Services
{
    public class SendMailService : ISendMailService
    {
        private const string _mailFrom = "maybesergxlove@gmail.com";
        private const string _mailFromPassword = "haek rdjd ufhg qmtw";
        private const string _server = "smtp.gmail.com";
        private readonly MimeMessage _email;
        public SendMailService()
        {
            _email = new MimeMessage();
        }
        public async Task<bool> SendEmailAsync(MailContent content, CancellationToken token)
        {
            try
            {
                _email.From.Add(new MailboxAddress("", _mailFrom));
                _email.To.Add(new MailboxAddress("", content.EmailTo));
                _email.Subject = BodyMails.GetSubject(content.Type);
                BodyBuilder body = new BodyBuilder();
                body.HtmlBody = BodyMails.GetBody(content.Type, content.Code);
                _email.Body = body.ToMessageBody();
                using var smtp = new SmtpClient();
                await smtp.ConnectAsync(_server, 587, SecureSocketOptions.StartTls, token);
                await smtp.AuthenticateAsync(_mailFrom, _mailFromPassword, token);
                await smtp.SendAsync(_email, token);
                await smtp.DisconnectAsync(true, token);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
