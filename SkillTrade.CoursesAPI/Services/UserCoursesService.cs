using SkillTrade.Core.Models;
using SkillTrade.CoursesAPI.Abstractions;
using SkillTrade.DataAccess.Postgres.Abstractions;

namespace SkillTrade.CoursesAPI.Services
{
    public class UserCoursesService : IUserCoursesService
    {
        private readonly IUserCoursesRepository _repository;
        public UserCoursesService(IUserCoursesRepository repository)
        {
            _repository = repository;
        }
        public async Task<Guid> CreateAsync(UserCourses userCourse, CancellationToken token)
        {
            return await _repository.CreateAsync(userCourse, token);
        }
        public async Task<int> DeleteAsync(Guid id, CancellationToken token)
        {
            return await _repository.DeleteAsync(id, token);
        }
        public async Task<IEnumerable<UserCourses>> GetActiveSubscriptionsByUserIdAsync(Guid userId,
            CancellationToken token)
        {
            return await _repository.GetActiveSubscriptionsByUserIdAsync(userId, token);
        }
        public async Task<IEnumerable<UserCourses>> GetByCourseIdAsync(Guid courseId, CancellationToken token)
        {
            return await _repository.GetByCourseIdAsync(courseId, token);
        }
        public async Task<UserCourses?> GetByIdAsync(Guid id, CancellationToken token)
        {
            return await _repository.GetByIdAsync(id, token);
        }
        public async Task<UserCourses?> GetByUserAndCourseAsync(Guid userId, Guid courseId, CancellationToken token)
        {
            return await _repository.GetByUserAndCourseAsync(userId, courseId, token);
        }
        public async Task<IEnumerable<UserCourses>> GetByUserIdAsync(Guid userId, CancellationToken token)
        {
            return await _repository.GetByUserIdAsync(userId, token);
        }
        public async Task<int> GetSubscribersCountAsync(Guid courseId, CancellationToken token)
        {
            return await _repository.GetSubscribersCountAsync(courseId, token);
        }
        public async Task<bool> IsUserSubscribedAsync(Guid userId, Guid courseId, CancellationToken token)
        {
            return await _repository.IsUserSubscribedAsync(userId, courseId, token);
        }
        public async Task<int> UpdateProgressAsync(Guid userCourseId, int newProgress, CancellationToken token)
        {
            return await _repository.UpdateProgressAsync(userCourseId, newProgress, token);
        }
        public async Task<int> GetProgressAsync(Guid courseId, CancellationToken token)
        {
            return await _repository.GetProgressAsync(courseId, token);
        }
    }
}
