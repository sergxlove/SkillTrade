using SkillTrade.Core.Requests;

namespace SkillTrade.Core.Abstractions
{
    public interface IJwtProviderService
    {
        string? GenerateToken(JwtRequest request);
    }
}