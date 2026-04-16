using SkillTrade.Core.Abstractions;
using SkillTrade.Core.Services;

namespace SkillTrade.Core.Models
{
    public class Users
    {
        public Guid Id { get; set; }
        public string Login { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string HashPassword { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public static IPasswordHasherService PasswordHasherService { get; set; }
            = new PasswordHasherService();


    }
}
