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

        public async Task<Courses?> GetByIdAsync(Guid id)
        {
            var entity = await _context.CoursesTable
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.Id == id);

            if (entity == null) return null;

            return Courses.Create(
                entity.Id, entity.IdActor, entity.Title, entity.Description,
                entity.Level, entity.Price, entity.LessonsCount,
                entity.DurationTimeHours, entity.CreatedAt).Value;
        }

        public async Task<IEnumerable<Courses>> GetAllAsync()
        {
            var entities = await _context.CoursesTable
                .AsNoTracking()
                .OrderByDescending(c => c.CreatedAt)
                .ToListAsync();

            return entities.Select(e => Courses.Create(
                e.Id, e.IdActor, e.Title, e.Description, e.Level,
                e.Price, e.LessonsCount, e.DurationTimeHours, e.CreatedAt).Value);
        }

        public async Task<IEnumerable<Courses>> GetByActorIdAsync(Guid actorId)
        {
            var entities = await _context.CoursesTable
                .AsNoTracking()
                .Where(c => c.IdActor == actorId)
                .OrderByDescending(c => c.CreatedAt)
                .ToListAsync();

            return entities.Select(e => Courses.Create(
                e.Id, e.IdActor, e.Title, e.Description, e.Level,
                e.Price, e.LessonsCount, e.DurationTimeHours, e.CreatedAt).Value);
        }

        public async Task<IEnumerable<Courses>> GetByLevelAsync(string level)
        {
            var entities = await _context.CoursesTable
                .AsNoTracking()
                .Where(c => c.Level.ToLower() == level.ToLower())
                .OrderByDescending(c => c.CreatedAt)
                .ToListAsync();

            return entities.Select(e => Courses.Create(
                e.Id, e.IdActor, e.Title, e.Description, e.Level,
                e.Price, e.LessonsCount, e.DurationTimeHours, e.CreatedAt).Value);
        }

        public async Task<IEnumerable<Courses>> SearchByTitleAsync(string searchTerm)
        {
            var entities = await _context.CoursesTable
                .AsNoTracking()
                .Where(c => EF.Functions.ILike(c.Title, $"%{searchTerm}%"))
                .OrderByDescending(c => c.CreatedAt)
                .ToListAsync();

            return entities.Select(e => Courses.Create(
                e.Id, e.IdActor, e.Title, e.Description, e.Level,
                e.Price, e.LessonsCount, e.DurationTimeHours, e.CreatedAt).Value);
        }

        public async Task<Guid> CreateAsync(Courses course)
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

            await _context.CoursesTable.AddAsync(entity);
            await _context.SaveChangesAsync();

            return entity.Id;
        }

        public async Task UpdateAsync(Courses course)
        {
            var entity = await _context.CoursesTable.FindAsync(course.Id);
            if (entity != null)
            {
                entity.Title = course.Title;
                entity.Description = course.Description;
                entity.Level = course.Level;
                entity.Price = course.Price;
                entity.LessonsCount = course.LessonsCount;
                entity.DurationTimeHours = course.DurationTimeHours;

                _context.CoursesTable.Update(entity);
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteAsync(Guid id)
        {
            var entity = await _context.CoursesTable.FindAsync(id);
            if (entity != null)
            {
                _context.CoursesTable.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> ExistsAsync(Guid id)
        {
            return await _context.CoursesTable.AnyAsync(c => c.Id == id);
        }

        public async Task<IEnumerable<Courses>> GetPagedAsync(int page, int pageSize)
        {
            var entities = await _context.CoursesTable
                .AsNoTracking()
                .OrderByDescending(c => c.CreatedAt)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return entities.Select(e => Courses.Create(
                e.Id, e.IdActor, e.Title, e.Description, e.Level,
                e.Price, e.LessonsCount, e.DurationTimeHours, e.CreatedAt).Value);
        }
    }
}
