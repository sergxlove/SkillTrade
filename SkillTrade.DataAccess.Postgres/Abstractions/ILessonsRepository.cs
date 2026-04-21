using SkillTrade.Core.Models;

namespace SkillTrade.DataAccess.Postgres.Abstractions
{
    public interface ILessonsRepository
    {
        Task<Guid> CreateAsync(Lessons lesson);
        Task DeleteAsync(Guid id);
        Task<bool> ExistsAsync(Guid id);
        Task<IEnumerable<Lessons>> GetByCourseIdAsync(Guid courseId);
        Task<IEnumerable<Lessons>> GetByCourseIdPagedAsync(Guid courseId, int page, int pageSize);
        Task<Lessons?> GetByIdAsync(Guid id);
        Task<int> GetLessonsCountByCourseIdAsync(Guid courseId);
        Task UpdateAsync(Lessons lesson);
    }
}