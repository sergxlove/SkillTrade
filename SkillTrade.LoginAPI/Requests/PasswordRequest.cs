namespace SkillTrade.LoginAPI.Requests
{
    public class PasswordRequest
    {
        public Guid Id { get; set; }
        public string NewPassword { get; set; } = string.Empty;
    }
}
