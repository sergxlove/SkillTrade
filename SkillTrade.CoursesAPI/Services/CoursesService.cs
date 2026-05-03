using SkillTrade.Core.Models;
using SkillTrade.CoursesAPI.Abstractions;
using SkillTrade.DataAccess.Postgres.Abstractions;

namespace SkillTrade.CoursesAPI.Services
{
    public class CoursesService : ICoursesService
    {
        private readonly ICoursesRepository _repository;
        public CoursesService(ICoursesRepository repository)
        {
            _repository = repository;
        }

        public async Task<Guid> CreateAsync(Courses course, CancellationToken token)
        {
            return await _repository.CreateAsync(course, token);
        }
        public async Task<int> DeleteAsync(Guid id, CancellationToken token)
        {
            return await _repository.DeleteAsync(id, token);
        }
        public async Task<bool> ExistsAsync(Guid id, CancellationToken token)
        {
            return await _repository.ExistsAsync(id, token);
        }
        public async Task<IEnumerable<Courses>> GetAllAsync(CancellationToken token)
        {
            return await _repository.GetAllAsync(token);
        }
        public async Task<IEnumerable<Courses>> GetByActorIdAsync(Guid actorId, CancellationToken token)
        {
            return await _repository.GetByActorIdAsync(actorId, token);
        }
        public async Task<Courses?> GetByIdAsync(Guid id, CancellationToken token)
        {
            return await _repository.GetByIdAsync(id, token);
        }
        public async Task<IEnumerable<Courses>> GetByLevelAsync(string level, CancellationToken token)
        {
            return await _repository.GetByLevelAsync(level, token);
        }
        public async Task<IEnumerable<Courses>> GetPagedAsync(int page, int pageSize, CancellationToken token)
        {
            return await _repository.GetPagedAsync(page, pageSize, token);
        }
        public async Task<IEnumerable<Courses>> SearchByTitleAsync(string searchTerm, CancellationToken token)
        {
            return await _repository.SearchByTitleAsync(searchTerm, token);
        }
        public async Task<int> UpdateAsync(Courses course, CancellationToken token)
        {
            return await _repository.UpdateAsync(course, token);
        }
    }
}
