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

        public async Task<Lessons?> GetByIdAsync(Guid id)
        {
            var entity = await _context.LessonsTable
                .AsNoTracking()
                .FirstOrDefaultAsync(l => l.Id == id);

            if (entity == null) return null;

            return Lessons.Create(
                entity.Id, entity.IdCourse, entity.Title,
                entity.Content, entity.CreatedAt).Value;
        }

        public async Task<IEnumerable<Lessons>> GetByCourseIdAsync(Guid courseId)
        {
            var entities = await _context.LessonsTable
                .AsNoTracking()
                .Where(l => l.IdCourse == courseId)
                .OrderBy(l => l.CreatedAt)
                .ToListAsync();

            return entities.Select(e => Lessons.Create(
                e.Id, e.IdCourse, e.Title, e.Content, e.CreatedAt).Value);
        }

        public async Task<IEnumerable<Lessons>> GetByCourseIdPagedAsync(Guid courseId, int page, int pageSize)
        {
            var entities = await _context.LessonsTable
                .AsNoTracking()
                .Where(l => l.IdCourse == courseId)
                .OrderBy(l => l.CreatedAt)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return entities.Select(e => Lessons.Create(
                e.Id, e.IdCourse, e.Title, e.Content, e.CreatedAt).Value);
        }

        public async Task<Guid> CreateAsync(Lessons lesson)
        {
            var entity = new LessonsEntity
            {
                Id = lesson.Id,
                IdCourse = lesson.IdCourse,
                Title = lesson.Title,
                Content = lesson.Content,
                CreatedAt = lesson.CreatedAt
            };

            await _context.LessonsTable.AddAsync(entity);
            await _context.SaveChangesAsync();

            return entity.Id;
        }

        public async Task UpdateAsync(Lessons lesson)
        {
            var entity = await _context.LessonsTable.FindAsync(lesson.Id);
            if (entity != null)
            {
                entity.Title = lesson.Title;
                entity.Content = lesson.Content;

                _context.LessonsTable.Update(entity);
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteAsync(Guid id)
        {
            var entity = await _context.LessonsTable.FindAsync(id);
            if (entity != null)
            {
                _context.LessonsTable.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<int> GetLessonsCountByCourseIdAsync(Guid courseId)
        {
            return await _context.LessonsTable
                .CountAsync(l => l.IdCourse == courseId);
        }

        public async Task<bool> ExistsAsync(Guid id)
        {
            return await _context.LessonsTable.AnyAsync(l => l.Id == id);
        }
    }
}
