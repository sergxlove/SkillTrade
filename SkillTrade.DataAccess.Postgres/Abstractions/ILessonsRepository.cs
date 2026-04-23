using SkillTrade.Core.Models;

namespace SkillTrade.DataAccess.Postgres.Abstractions
{
    public interface ILessonsRepository
    {
        Task<Guid> CreateAsync(Lessons lesson, CancellationToken token);
        Task DeleteAsync(Guid id, CancellationToken token);
        Task<bool> ExistsAsync(Guid id, CancellationToken token);
        Task<IEnumerable<Lessons>> GetByCourseIdAsync(Guid courseId, CancellationToken token);
        Task<IEnumerable<Lessons>> GetByCourseIdPagedAsync(Guid courseId, int page, int pageSize, CancellationToken token);
        Task<Lessons?> GetByIdAsync(Guid id, CancellationToken token);
        Task<int> GetLessonsCountByCourseIdAsync(Guid courseId, CancellationToken token);
        Task UpdateAsync(Lessons lesson, CancellationToken token);
    }
}