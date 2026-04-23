using Microsoft.EntityFrameworkCore;
using SkillTrade.Core.Models;
using SkillTrade.DataAccess.Postgres.Abstractions;
using SkillTrade.DataAccess.Postgres.Models;

namespace SkillTrade.DataAccess.Postgres.Repositories
{
    public class LessonsRepository : ILessonsRepository
    {
        private readonly SkillTradeDbContext _context;

        public LessonsRepository(SkillTradeDbContext context)
        {
            _context = context;
        }

        public async Task<Lessons?> GetByIdAsync(Guid id, CancellationToken token)
        {
            var entity = await _context.LessonsTable
                .AsNoTracking()
                .FirstOrDefaultAsync(l => l.Id == id, token);

            if (entity == null) return null;

            return Lessons.Create(
                entity.Id, entity.IdCourse, entity.Title,
                entity.Content, entity.CreatedAt).Value;
        }

        public async Task<IEnumerable<Lessons>> GetByCourseIdAsync(Guid courseId, CancellationToken token)
        {
            var entities = await _context.LessonsTable
                .AsNoTracking()
                .Where(l => l.IdCourse == courseId)
                .OrderBy(l => l.CreatedAt)
                .ToListAsync(token);

            return entities.Select(e => Lessons.Create(
                e.Id, e.IdCourse, e.Title, e.Content, e.CreatedAt).Value);
        }

        public async Task<IEnumerable<Lessons>> GetByCourseIdPagedAsync(Guid courseId, int page, int pageSize, CancellationToken token)
        {
            var entities = await _context.LessonsTable
                .AsNoTracking()
                .Where(l => l.IdCourse == courseId)
                .OrderBy(l => l.CreatedAt)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(token);

            return entities.Select(e => Lessons.Create(
                e.Id, e.IdCourse, e.Title, e.Content, e.CreatedAt).Value);
        }

        public async Task<Guid> CreateAsync(Lessons lesson, CancellationToken token)
        {
            var entity = new LessonsEntity
            {
                Id = lesson.Id,
                IdCourse = lesson.IdCourse,
                Title = lesson.Title,
                Content = lesson.Content,
                CreatedAt = lesson.CreatedAt
            };

            await _context.LessonsTable.AddAsync(entity, token);
            await _context.SaveChangesAsync(token);

            return entity.Id;
        }

        public async Task UpdateAsync(Lessons lesson, CancellationToken token)
        {
            var entity = await _context.LessonsTable.FindAsync(new object[] { lesson.Id }, token);
            if (entity != null)
            {
                entity.Title = lesson.Title;
                entity.Content = lesson.Content;

                _context.LessonsTable.Update(entity);
                await _context.SaveChangesAsync(token);
            }
        }

        public async Task DeleteAsync(Guid id, CancellationToken token)
        {
            var entity = await _context.LessonsTable.FindAsync(new object[] { id }, token);
            if (entity != null)
            {
                _context.LessonsTable.Remove(entity);
                await _context.SaveChangesAsync(token);
            }
        }

        public async Task<int> GetLessonsCountByCourseIdAsync(Guid courseId, CancellationToken token)
        {
            return await _context.LessonsTable
                .CountAsync(l => l.IdCourse == courseId, token);
        }

        public async Task<bool> ExistsAsync(Guid id, CancellationToken token)
        {
            return await _context.LessonsTable.AnyAsync(l => l.Id == id, token);
        }
    }
}