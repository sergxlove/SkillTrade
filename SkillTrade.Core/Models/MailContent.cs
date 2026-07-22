using SkillTrade.Core.Infrastructures;

namespace SkillTrade.Core.Models
{
    public class MailContent
    {
        public const int MIN_LENGTH_CODE = 4;
        public Guid Id { get; }
        public MailType Type { get; }
        public string EmailTo { get; } = string.Empty;
        public string Code { get; } = string.Empty;

        private MailContent(Guid id, MailType type, string emailTo, string code)
        {
            Id = id;
            Type = type;
            EmailTo = emailTo;
            Code = code;
        }

        public static ResultModel<MailContent> Create(Guid id, MailType type, string emailTo, string code)
        {
            if (id == Guid.Empty)
                return ResultModel<MailContent>.Failure("Поле Id не должно быть пустым");
            if (type == MailType.None)
                return ResultModel<MailContent>.Failure("Поле MailType должно иметь значение");
            if (emailTo == string.Empty)
                return ResultModel<MailContent>.Failure("Поле EmailTo не должно быть пустым");
            if (code.Length < MIN_LENGTH_CODE)
                return ResultModel<MailContent>.Failure($"Поле Code должно содержать {MIN_LENGTH_CODE} и " +
                    $"более символов");
            return ResultModel<MailContent>.Success(new(id, type, emailTo, code));
        }

        public static ResultModel<MailContent> Create(MailType type, string emailTo, string code)
        {
            return Create(Guid.NewGuid(), type, emailTo, code);
        }
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
