using SkillTrade.Core.Models;

namespace SkillTrade.LoginAPI.Abstractions
{
    public interface IVerifyOperationService
    {
        Task<Guid> AddAsync(VerifyOperations oper, CancellationToken token);
        Task<int> DeleteAsync(string email, CancellationToken token);
        Task<int> IncrementTryAsync(string email, CancellationToken token);
        Task<bool> VerifyAsync(VerifyOperations oper, int minutesValid, CancellationToken token);
    }
}