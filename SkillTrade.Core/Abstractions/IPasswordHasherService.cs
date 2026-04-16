namespace SkillTrade.Core.Abstractions
{
    public interface IPasswordHasherService
    {
        string HashBCrypt(string password);
        bool VerifyBCrypt(string password, string hashedPassword);
    }
}
