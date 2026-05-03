using SkillTrade.Core.Models;

namespace SkillTrade.CoursesAPI.Abstractions
{
    public interface ICoursesService
    {
        Task<Guid> CreateAsync(Courses course, CancellationToken token);
        Task<int> DeleteAsync(Guid id, CancellationToken token);
        Task<bool> ExistsAsync(Guid id, CancellationToken token);
        Task<IEnumerable<Courses>> GetAllAsync(CancellationToken token);
        Task<IEnumerable<Courses>> GetByActorIdAsync(Guid actorId, CancellationToken token);
        Task<Courses?> GetByIdAsync(Guid id, CancellationToken token);
        Task<IEnumerable<Courses>> GetByLevelAsync(string level, CancellationToken token);
        Task<IEnumerable<Courses>> GetPagedAsync(int page, int pageSize, CancellationToken token);
        Task<IEnumerable<Courses>> SearchByTitleAsync(string searchTerm, CancellationToken token);
        Task<int> UpdateAsync(Courses course, CancellationToken token);
    }
}