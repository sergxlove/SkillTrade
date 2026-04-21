using SkillTrade.Core.Models;

namespace SkillTrade.DataAccess.Postgres.Abstractions
{
    public interface IUserCoursesRepository
    {
        Task<Guid> CreateAsync(UserCourses userCourse);
        Task DeleteAsync(Guid id);
        Task<IEnumerable<UserCourses>> GetActiveSubscriptionsByUserIdAsync(Guid userId);
        Task<IEnumerable<UserCourses>> GetByCourseIdAsync(Guid courseId);
        Task<UserCourses?> GetByIdAsync(Guid id);
        Task<UserCourses?> GetByUserAndCourseAsync(Guid userId, Guid courseId);
        Task<IEnumerable<UserCourses>> GetByUserIdAsync(Guid userId);
        Task<int> GetSubscribersCountAsync(Guid courseId);
        Task<bool> IsUserSubscribedAsync(Guid userId, Guid courseId);
        Task UpdateProgressAsync(Guid userCourseId, int newProgress);
    }
}