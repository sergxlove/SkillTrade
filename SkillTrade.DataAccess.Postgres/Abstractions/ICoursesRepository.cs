using SkillTrade.Core.Models;

namespace SkillTrade.DataAccess.Postgres.Abstractions
{
    public interface ICoursesRepository
    {
        Task<Guid> CreateAsync(Courses course);
        Task DeleteAsync(Guid id);
        Task<bool> ExistsAsync(Guid id);
        Task<IEnumerable<Courses>> GetAllAsync();
        Task<IEnumerable<Courses>> GetByActorIdAsync(Guid actorId);
        Task<Courses?> GetByIdAsync(Guid id);
        Task<IEnumerable<Courses>> GetByLevelAsync(string level);
        Task<IEnumerable<Courses>> GetPagedAsync(int page, int pageSize);
        Task<IEnumerable<Courses>> SearchByTitleAsync(string searchTerm);
        Task UpdateAsync(Courses course);
    }
}