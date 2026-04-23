using SkillTrade.Core.Models;

namespace SkillTrade.DataAccess.Postgres.Abstractions
{
    public interface ICoursesRepository
    {
        Task<Guid> CreateAsync(Courses course, CancellationToken token);
        Task DeleteAsync(Guid id, CancellationToken token);
        Task<bool> ExistsAsync(Guid id, CancellationToken token);
        Task<IEnumerable<Courses>> GetAllAsync(CancellationToken token);
        Task<IEnumerable<Courses>> GetByActorIdAsync(Guid actorId, CancellationToken token);
        Task<Courses?> GetByIdAsync(Guid id, CancellationToken token);
        Task<IEnumerable<Courses>> GetByLevelAsync(string level, CancellationToken token);
        Task<IEnumerable<Courses>> GetPagedAsync(int page, int pageSize, CancellationToken token);
        Task<IEnumerable<Courses>> SearchByTitleAsync(string searchTerm, CancellationToken token);
        Task UpdateAsync(Courses course, CancellationToken token);
    }
}