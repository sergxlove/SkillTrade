using SkillTrade.Core.Models;

namespace SkillTrade.DataAccess.Postgres.Abstractions
{
    public interface IVerifyOperationsRepository
    {
        Task<Guid> AddAsync(VerifyOperations oper, CancellationToken token);
        Task<int> DeleteAsync(string email, CancellationToken token);
        Task<int> IncrementTryAsync(string email, CancellationToken token);
        Task<bool> VerifyAsync(VerifyOperations oper, int minutesValid, CancellationToken token);
    }
}