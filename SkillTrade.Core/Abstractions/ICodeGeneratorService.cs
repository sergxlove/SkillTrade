namespace SkillTrade.Core.Abstractions
{
    public interface ICodeGeneratorService
    {
        string GenerateCode(int length, bool isDigits = true, bool isChars = true);
    }
}