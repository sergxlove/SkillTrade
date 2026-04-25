using SkillTrade.Core.Models;
using SkillTrade.DataAccess.Postgres.Abstractions;
using SkillTrade.LoginAPI.Abstractions;

namespace SkillTrade.LoginAPI.Services
{
    public class UsersService : IUsersService
    {
        private readonly IUsersRepository _repository;
        public UsersService(IUsersRepository repository)
        {
            _repository = repository;
        }
        public async Task<Guid> CreateAsync(Users user, CancellationToken token)
        {
            return await _repository.CreateAsync(user, token);
        }
        public async Task<int> DeleteAsync(Guid id, CancellationToken token)
        {
            return await _repository.DeleteAsync(id, token);
        }
        public async Task<bool> ExistsByIdAsync(Guid id, CancellationToken token)
        {
            return await _repository.ExistsByIdAsync(id, token);
        }
        public async Task<bool> ExistsByLoginAsync(string login, CancellationToken token)
        {
            return await _repository.ExistsByLoginAsync(login, token);
        }
        public async Task<IEnumerable<Users>> GetAllAsync(CancellationToken token)
        {
            return await _repository.GetAllAsync(token);
        }
        public async Task<Users?> GetByIdAsync(Guid id, CancellationToken token)
        {
            return await _repository.GetByIdAsync(id, token);
        }
        public async Task<Users?> GetByLoginAsync(string login, CancellationToken token)
        {
            return await _repository.GetByLoginAsync(login, token);
        }
        public async Task<IEnumerable<Users>> GetByRoleAsync(string role, CancellationToken token)
        {
            return await _repository.GetByRoleAsync(role, token);
        }
        public async Task<IEnumerable<Users>> GetPagedAsync(int page, int pageSize, CancellationToken token)
        {
            return await _repository.GetPagedAsync(page, pageSize, token);
        }
        public async Task<int> UpdateAsync(Users user, CancellationToken token)
        {
            return await _repository.UpdateAsync(user, token);
        }
        public async Task<int> UpdateBalanceAsync(Guid userId, decimal newBalance, CancellationToken token)
        {
            return await _repository.UpdateBalanceAsync(userId, newBalance, token);
        }
        public async Task<bool> VerifyAsync(string login, string password, CancellationToken token)
        {
            return await _repository.VerifyAsync(login, password, token);
        }
        public async Task<string> GetRoleAsync(string login, CancellationToken token)
        {
            return await _repository.GetRoleAsync(login, token);
        }
    }
}
