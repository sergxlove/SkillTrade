using SkillTrade.LoginAPI.Requests;

namespace SkillTrade.LoginAPI.Abstractions
{
    public interface IJwtProviderService
    {
        string? GenerateToken(JwtRequest request);
    }
}