namespace SkillTrade.Core.Models
{
    public class MailContent
    {
        public Guid Id { get; }
        public MailType Type { get; }
        public string EmailTo { get; } = string.Empty;
        public string Code { get; } = string.Empty;
    }

    public enum MailType
    {
        None = 0,
        Transact = 1,
        Restore = 2,
        Verify = 3,
        Enter = 4,
    }
}
