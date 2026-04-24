using Microsoft.EntityFrameworkCore;
using SkillTrade.Core.Models;
using SkillTrade.DataAccess.Postgres.Abstractions;
using SkillTrade.DataAccess.Postgres.Models;

namespace SkillTrade.DataAccess.Postgres.Repositories
{
    public class UserCoursesRepository : IUserCoursesRepository
    {
        private readonly SkillTradeDbContext _context;

        public UserCoursesRepository(SkillTradeDbContext context)
        {
            _context = context;
        }

        public async Task<UserCourses?> GetByIdAsync(Guid id, CancellationToken token)
        {
            var entity = await _context.UserCoursesTable
                .AsNoTracking()
                .FirstOrDefaultAsync(uc => uc.Id == id, token);

            if (entity == null) return null;

            return UserCourses.Create(
                entity.Id, entity.UserId, entity.CourseId,
                entity.CurrentProgress, entity.TotalProgress,
                entity.SubscribeTime).Value;
        }

        public async Task<UserCourses?> GetByUserAndCourseAsync(Guid userId, Guid courseId, CancellationToken token)
        {
            var entity = await _context.UserCoursesTable
                .AsNoTracking()
                .FirstOrDefaultAsync(uc => uc.UserId == userId && uc.CourseId == courseId, token);

            if (entity == null) return null;

            return UserCourses.Create(
                entity.Id, entity.UserId, entity.CourseId,
                entity.CurrentProgress, entity.TotalProgress,
                entity.SubscribeTime).Value;
        }

        public async Task<IEnumerable<UserCourses>> GetByUserIdAsync(Guid userId, CancellationToken token)
        {
            var entities = await _context.UserCoursesTable
                .AsNoTracking()
                .Where(uc => uc.UserId == userId)
                .OrderByDescending(uc => uc.SubscribeTime)
                .ToListAsync(token);

            return entities.Select(e => UserCourses.Create(
                e.Id, e.UserId, e.CourseId, e.CurrentProgress,
                e.TotalProgress, e.SubscribeTime).Value);
        }

        public async Task<IEnumerable<UserCourses>> GetByCourseIdAsync(Guid courseId, CancellationToken token)
        {
            var entities = await _context.UserCoursesTable
                .AsNoTracking()
                .Where(uc => uc.CourseId == courseId)
                .OrderByDescending(uc => uc.SubscribeTime)
                .ToListAsync(token);

            return entities.Select(e => UserCourses.Create(
                e.Id, e.UserId, e.CourseId, e.CurrentProgress,
                e.TotalProgress, e.SubscribeTime).Value);
        }

        public async Task<IEnumerable<UserCourses>> GetActiveSubscriptionsByUserIdAsync(Guid userId, CancellationToken token)
        {
            var entities = await _context.UserCoursesTable
                .AsNoTracking()
                .Where(uc => uc.UserId == userId && uc.CurrentProgress < uc.TotalProgress)
                .OrderByDescending(uc => uc.SubscribeTime)
                .ToListAsync(token);

            return entities.Select(e => UserCourses.Create(
                e.Id, e.UserId, e.CourseId, e.CurrentProgress,
                e.TotalProgress, e.SubscribeTime).Value);
        }

        public async Task<Guid> CreateAsync(UserCourses userCourse, CancellationToken token)
        {
            var entity = new UserCoursesEntity
            {
                Id = userCourse.Id,
                UserId = userCourse.UserId,
                CourseId = userCourse.CourseId,
                CurrentProgress = userCourse.CurrentProgress,
                TotalProgress = userCourse.TotalProgress,
                SubscribeTime = userCourse.SubscribeTime
            };

            await _context.UserCoursesTable.AddAsync(entity, token);
            await _context.SaveChangesAsync(token);

            return entity.Id;
        }

        public async Task<int> UpdateProgressAsync(Guid userCourseId, int newProgress, CancellationToken token)
        {
            return await _context.UserCoursesTable
                .AsNoTracking()
                .Where(a => a.Id == userCourseId)
                .ExecuteUpdateAsync(a => a
                .SetProperty(a => a.CurrentProgress, newProgress), token);
        }

        public async Task<int> DeleteAsync(Guid id, CancellationToken token)
        {
            return await _context.UserCoursesTable
                .AsNoTracking()
                .Where(a => a.Id == id)
                .ExecuteDeleteAsync(token);
        }

        public async Task<bool> IsUserSubscribedAsync(Guid userId, Guid courseId, CancellationToken token)
        {
            return await _context.UserCoursesTable
                .AnyAsync(uc => uc.UserId == userId && uc.CourseId == courseId, token);
        }

        public async Task<int> GetSubscribersCountAsync(Guid courseId, CancellationToken token)
        {
            return await _context.UserCoursesTable
                .CountAsync(uc => uc.CourseId == courseId, token);
        }
    }
}