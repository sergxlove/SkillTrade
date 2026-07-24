using SkillTrade.Core.Models;
using SkillTrade.DataAccess.Postgres.Abstractions;
using SkillTrade.LoginAPI.Abstractions;

namespace SkillTrade.LoginAPI.Services
{
    public class VerifyOperationService : IVerifyOperationService
    {
        private readonly IVerifyOperationsRepository _repository;
        public VerifyOperationService(IVerifyOperationsRepository repository)
        {
            _repository = repository;
        }
        public async Task<Guid> AddAsync(VerifyOperations oper, CancellationToken token)
        {
            return await _repository.AddAsync(oper, token);
        }
        public async Task<int> DeleteAsync(string email, CancellationToken token)
        {
            return await _repository.DeleteAsync(email, token);
        }
        public async Task<int> IncrementTryAsync(string email, CancellationToken token)
        {
            return await _repository.IncrementTryAsync(email, token);
        }
        public async Task<bool> VerifyAsync(VerifyOperations oper, int minutesValid, CancellationToken token)
        {
            return await _repository.VerifyAsync(oper, minutesValid, token);
        }
    }
}
