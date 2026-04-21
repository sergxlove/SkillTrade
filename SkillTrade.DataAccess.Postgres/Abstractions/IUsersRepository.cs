using SkillTrade.Core.Models;

namespace SkillTrade.DataAccess.Postgres.Abstractions
{
    public interface IUsersRepository
    {
        Task<Guid> CreateAsync(Users user);
        Task DeleteAsync(Guid id);
        Task<bool> ExistsByIdAsync(Guid id);
        Task<bool> ExistsByLoginAsync(string login);
        Task<IEnumerable<Users>> GetAllAsync();
        Task<Users?> GetByIdAsync(Guid id);
        Task<Users?> GetByLoginAsync(string login);
        Task<IEnumerable<Users>> GetByRoleAsync(string role);
        Task<IEnumerable<Users>> GetPagedAsync(int page, int pageSize);
        Task UpdateAsync(Users user);
        Task UpdateBalanceAsync(Guid userId, decimal newBalance);
    }
}