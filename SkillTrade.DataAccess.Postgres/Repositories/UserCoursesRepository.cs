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

        public async Task<UserCourses?> GetByIdAsync(Guid id)
        {
            var entity = await _context.UserCoursesTable
                .AsNoTracking()
                .FirstOrDefaultAsync(uc => uc.Id == id);

            if (entity == null) return null;

            return UserCourses.Create(
                entity.Id, entity.UserId, entity.CourseId,
                entity.CurrentProgress, entity.TotalProgress,
                entity.SubscribeTime).Value;
        }

        public async Task<UserCourses?> GetByUserAndCourseAsync(Guid userId, Guid courseId)
        {
            var entity = await _context.UserCoursesTable
                .AsNoTracking()
                .FirstOrDefaultAsync(uc => uc.UserId == userId && uc.CourseId == courseId);

            if (entity == null) return null;

            return UserCourses.Create(
                entity.Id, entity.UserId, entity.CourseId,
                entity.CurrentProgress, entity.TotalProgress,
                entity.SubscribeTime).Value;
        }

        public async Task<IEnumerable<UserCourses>> GetByUserIdAsync(Guid userId)
        {
            var entities = await _context.UserCoursesTable
                .AsNoTracking()
                .Where(uc => uc.UserId == userId)
                .OrderByDescending(uc => uc.SubscribeTime)
                .ToListAsync();

            return entities.Select(e => UserCourses.Create(
                e.Id, e.UserId, e.CourseId, e.CurrentProgress,
                e.TotalProgress, e.SubscribeTime).Value);
        }

        public async Task<IEnumerable<UserCourses>> GetByCourseIdAsync(Guid courseId)
        {
            var entities = await _context.UserCoursesTable
                .AsNoTracking()
                .Where(uc => uc.CourseId == courseId)
                .OrderByDescending(uc => uc.SubscribeTime)
                .ToListAsync();

            return entities.Select(e => UserCourses.Create(
                e.Id, e.UserId, e.CourseId, e.CurrentProgress,
                e.TotalProgress, e.SubscribeTime).Value);
        }

        public async Task<IEnumerable<UserCourses>> GetActiveSubscriptionsByUserIdAsync(Guid userId)
        {
            var entities = await _context.UserCoursesTable
                .AsNoTracking()
                .Where(uc => uc.UserId == userId && uc.CurrentProgress < uc.TotalProgress)
                .OrderByDescending(uc => uc.SubscribeTime)
                .ToListAsync();

            return entities.Select(e => UserCourses.Create(
                e.Id, e.UserId, e.CourseId, e.CurrentProgress,
                e.TotalProgress, e.SubscribeTime).Value);
        }

        public async Task<Guid> CreateAsync(UserCourses userCourse)
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

            await _context.UserCoursesTable.AddAsync(entity);
            await _context.SaveChangesAsync();

            return entity.Id;
        }

        public async Task UpdateProgressAsync(Guid userCourseId, int newProgress)
        {
            var entity = await _context.UserCoursesTable.FindAsync(userCourseId);
            if (entity != null && newProgress <= entity.TotalProgress)
            {
                entity.CurrentProgress = newProgress;
                _context.UserCoursesTable.Update(entity);
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteAsync(Guid id)
        {
            var entity = await _context.UserCoursesTable.FindAsync(id);
            if (entity != null)
            {
                _context.UserCoursesTable.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> IsUserSubscribedAsync(Guid userId, Guid courseId)
        {
            return await _context.UserCoursesTable
                .AnyAsync(uc => uc.UserId == userId && uc.CourseId == courseId);
        }

        public async Task<int> GetSubscribersCountAsync(Guid courseId)
        {
            return await _context.UserCoursesTable
                .CountAsync(uc => uc.CourseId == courseId);
        }
    }
}
