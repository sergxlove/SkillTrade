using Microsoft.EntityFrameworkCore;
using SkillTrade.Core.Models;
using SkillTrade.DataAccess.Postgres.Abstractions;
using SkillTrade.DataAccess.Postgres.Models;

namespace SkillTrade.DataAccess.Postgres.Repositories
{
    public class CoursesRepository : ICoursesRepository
    {
        private readonly SkillTradeDbContext _context;

        public CoursesRepository(SkillTradeDbContext context)
        {
            _context = context;
        }

        public async Task<Courses?> GetByIdAsync(Guid id, CancellationToken token)
        {
            var entity = await _context.CoursesTable
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.Id == id, token);

            if (entity == null) return null;

            return Courses.Create(
                entity.Id, entity.IdActor, entity.Title, entity.Description,
                entity.Level, entity.Price, entity.LessonsCount,
                entity.DurationTimeHours, entity.CreatedAt).Value;
        }

        public async Task<IEnumerable<Courses>> GetAllAsync(CancellationToken token)
        {
            var entities = await _context.CoursesTable
                .AsNoTracking()
                .OrderByDescending(c => c.CreatedAt)
                .ToListAsync(token);

            return entities.Select(e => Courses.Create(
                e.Id, e.IdActor, e.Title, e.Description, e.Level,
                e.Price, e.LessonsCount, e.DurationTimeHours, e.CreatedAt).Value);
        }

        public async Task<IEnumerable<Courses>> GetByActorIdAsync(Guid actorId, CancellationToken token)
        {
            var entities = await _context.CoursesTable
                .AsNoTracking()
                .Where(c => c.IdActor == actorId)
                .OrderByDescending(c => c.CreatedAt)
                .ToListAsync(token);

            return entities.Select(e => Courses.Create(
                e.Id, e.IdActor, e.Title, e.Description, e.Level,
                e.Price, e.LessonsCount, e.DurationTimeHours, e.CreatedAt).Value);
        }

        public async Task<IEnumerable<Courses>> GetByLevelAsync(string level, CancellationToken token)
        {
            var entities = await _context.CoursesTable
                .AsNoTracking()
                .Where(c => c.Level.ToLower() == level.ToLower())
                .OrderByDescending(c => c.CreatedAt)
                .ToListAsync(token);

            return entities.Select(e => Courses.Create(
                e.Id, e.IdActor, e.Title, e.Description, e.Level,
                e.Price, e.LessonsCount, e.DurationTimeHours, e.CreatedAt).Value);
        }

        public async Task<IEnumerable<Courses>> SearchByTitleAsync(string searchTerm, CancellationToken token)
        {
            var entities = await _context.CoursesTable
                .AsNoTracking()
                .Where(c => EF.Functions.ILike(c.Title, $"%{searchTerm}%"))
                .OrderByDescending(c => c.CreatedAt)
                .ToListAsync(token);

            return entities.Select(e => Courses.Create(
                e.Id, e.IdActor, e.Title, e.Description, e.Level,
                e.Price, e.LessonsCount, e.DurationTimeHours, e.CreatedAt).Value);
        }

        public async Task<Guid> CreateAsync(Courses course, CancellationToken token)
        {
            var entity = new CoursesEntity
            {
                Id = course.Id,
                IdActor = course.IdActor,
                Title = course.Title,
                Description = course.Description,
                Level = course.Level,
                Price = course.Price,
                LessonsCount = course.LessonsCount,
                DurationTimeHours = course.DurationTimeHours,
                CreatedAt = course.CreatedAt
            };

            await _context.CoursesTable.AddAsync(entity, token);
            await _context.SaveChangesAsync(token);

            return entity.Id;
        }

        public async Task UpdateAsync(Courses course, CancellationToken token)
        {
            var entity = await _context.CoursesTable.FindAsync(new object[] { course.Id }, token);
            if (entity != null)
            {
                entity.Title = course.Title;
                entity.Description = course.Description;
                entity.Level = course.Level;
                entity.Price = course.Price;
                entity.LessonsCount = course.LessonsCount;
                entity.DurationTimeHours = course.DurationTimeHours;

                _context.CoursesTable.Update(entity);
                await _context.SaveChangesAsync(token);
            }
        }

        public async Task DeleteAsync(Guid id, CancellationToken token)
        {
            var entity = await _context.CoursesTable.FindAsync(new object[] { id }, token);
            if (entity != null)
            {
                _context.CoursesTable.Remove(entity);
                await _context.SaveChangesAsync(token);
            }
        }

        public async Task<bool> ExistsAsync(Guid id, CancellationToken token)
        {
            return await _context.CoursesTable.AnyAsync(c => c.Id == id, token);
        }

        public async Task<IEnumerable<Courses>> GetPagedAsync(int page, int pageSize, CancellationToken token)
        {
            var entities = await _context.CoursesTable
                .AsNoTracking()
                .OrderByDescending(c => c.CreatedAt)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(token);

            return entities.Select(e => Courses.Create(
                e.Id, e.IdActor, e.Title, e.Description, e.Level,
                e.Price, e.LessonsCount, e.DurationTimeHours, e.CreatedAt).Value);
        }
    }
}