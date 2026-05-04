using SkillTrade.Core.Models;

namespace SkillTrade.DataAccess.Postgres.Abstractions
{
    public interface IUserCoursesRepository
    {
        Task<Guid> CreateAsync(UserCourses userCourse, CancellationToken token);
        Task<int> DeleteAsync(Guid id, CancellationToken token);
        Task<IEnumerable<UserCourses>> GetActiveSubscriptionsByUserIdAsync(Guid userId, CancellationToken token);
        Task<IEnumerable<UserCourses>> GetByCourseIdAsync(Guid courseId, CancellationToken token);
        Task<UserCourses?> GetByIdAsync(Guid id, CancellationToken token);
        Task<UserCourses?> GetByUserAndCourseAsync(Guid userId, Guid courseId, CancellationToken token);
        Task<IEnumerable<UserCourses>> GetByUserIdAsync(Guid userId, CancellationToken token);
        Task<int> GetSubscribersCountAsync(Guid courseId, CancellationToken token);
        Task<bool> IsUserSubscribedAsync(Guid userId, Guid courseId, CancellationToken token);
        Task<int> UpdateProgressAsync(Guid userCourseId, int newProgress, CancellationToken token);
        Task<int> GetProgressAsync(Guid courseId, CancellationToken token);
        Task<int> CountStartedCoursesAsync(Guid userId, CancellationToken token);
        Task<int> CountEndedCoursesAsync(Guid userId, CancellationToken token);
    }
}