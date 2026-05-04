using SkillTrade.Core.Models;

namespace SkillTrade.LoginAPI.Abstractions
{
    public interface IUsersService
    {
        Task<Guid> CreateAsync(Users user, CancellationToken token);
        Task<int> DeleteAsync(Guid id, CancellationToken token);
        Task<bool> ExistsByIdAsync(Guid id, CancellationToken token);
        Task<bool> ExistsByLoginAsync(string login, CancellationToken token);
        Task<IEnumerable<Users>> GetAllAsync(CancellationToken token);
        Task<Users?> GetByIdAsync(Guid id, CancellationToken token);
        Task<Users?> GetByLoginAsync(string login, CancellationToken token);
        Task<IEnumerable<Users>> GetByRoleAsync(string role, CancellationToken token);
        Task<IEnumerable<Users>> GetPagedAsync(int page, int pageSize, CancellationToken token);
        Task<int> UpdateAsync(Users user, CancellationToken token);
        Task<int> UpdateBalanceAsync(Guid userId, decimal newBalance, CancellationToken token);
        Task<bool> VerifyAsync(string login, string password, CancellationToken token);
        Task<string> GetRoleAsync(string login, CancellationToken token);
        Task<Guid> GetIdAsync(string login, CancellationToken token);
        Task<decimal> GetBalanceAsync(Guid id, CancellationToken token);
        Task<int> UpdatePasswordAsync(Guid id, string hashNewPassword, CancellationToken token);
    }
}