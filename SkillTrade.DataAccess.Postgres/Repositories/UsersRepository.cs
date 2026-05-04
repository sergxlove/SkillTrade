using Microsoft.EntityFrameworkCore;
using SkillTrade.Core.Models;
using SkillTrade.DataAccess.Postgres.Abstractions;
using SkillTrade.DataAccess.Postgres.Models;

namespace SkillTrade.DataAccess.Postgres.Repositories
{
    public class UsersRepository : IUsersRepository
    {
        private readonly SkillTradeDbContext _context;

        public UsersRepository(SkillTradeDbContext context)
        {
            _context = context;
        }

        public async Task<Users?> GetByIdAsync(Guid id, CancellationToken token)
        {
            var entity = await _context.UsersTable
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Id == id, token);

            if (entity == null) return null;

            return Users.Create(
                entity.Id, entity.Login, entity.Name, entity.HashPassword,
                entity.Role, entity.Balance, entity.CreatedAt).Value;
        }

        public async Task<Users?> GetByLoginAsync(string login, CancellationToken token)
        {
            var entity = await _context.UsersTable
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Login.ToLower() == login.ToLower(), token);

            if (entity == null) return null;

            return Users.Create(
                entity.Id, entity.Login, entity.Name, entity.HashPassword,
                entity.Role, entity.Balance, entity.CreatedAt).Value;
        }

        public async Task<IEnumerable<Users>> GetAllAsync(CancellationToken token)
        {
            var entities = await _context.UsersTable
                .AsNoTracking()
                .OrderByDescending(u => u.CreatedAt)
                .ToListAsync(token);

            return entities.Select(e => Users.Create(
                e.Id, e.Login, e.Name, e.HashPassword,
                e.Role, e.Balance, e.CreatedAt).Value);
        }

        public async Task<IEnumerable<Users>> GetByRoleAsync(string role, CancellationToken token)
        {
            var entities = await _context.UsersTable
                .AsNoTracking()
                .Where(u => u.Role.ToLower() == role.ToLower())
                .OrderByDescending(u => u.CreatedAt)
                .ToListAsync(token);

            return entities.Select(e => Users.Create(
                e.Id, e.Login, e.Name, e.HashPassword,
                e.Role, e.Balance, e.CreatedAt).Value);
        }

        public async Task<Guid> CreateAsync(Users user, CancellationToken token)
        {
            var entity = new UsersEntity
            {
                Id = user.Id,
                Login = user.Login,
                Name = user.Name,
                HashPassword = user.HashPassword,
                Role = user.Role,
                Balance = user.Balance,
                CreatedAt = user.CreatedAt
            };

            await _context.UsersTable.AddAsync(entity, token);
            await _context.SaveChangesAsync(token);

            return entity.Id;
        }

        public async Task<int> UpdateAsync(Users user, CancellationToken token)
        {
            return await _context.UsersTable
                .Where(a => a.Id == user.Id)
                .ExecuteUpdateAsync(a => a
                .SetProperty(a => a.HashPassword, user.HashPassword), token);
        }

        public async Task<int> UpdateBalanceAsync(Guid userId, decimal newBalance, CancellationToken token)
        {
            return await _context.UsersTable
                .Where(a => a.Id == userId)
                .ExecuteUpdateAsync(a => a
                .SetProperty(a => a.Balance, newBalance), token);
        }

        public async Task<int> DeleteAsync(Guid id, CancellationToken token)
        {
            return await _context.UsersTable
                .Where(a => a.Id == id)
                .ExecuteDeleteAsync(token);
        }

        public async Task<bool> ExistsByLoginAsync(string login, CancellationToken token)
        {
            return await _context.UsersTable
                .AnyAsync(u => u.Login.ToLower() == login.ToLower(), token);
        }

        public async Task<bool> ExistsByIdAsync(Guid id, CancellationToken token)
        {
            return await _context.UsersTable.AnyAsync(u => u.Id == id, token);
        }

        public async Task<IEnumerable<Users>> GetPagedAsync(int page, int pageSize, CancellationToken token)
        {
            var entities = await _context.UsersTable
                .AsNoTracking()
                .OrderByDescending(u => u.CreatedAt)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(token );

            return entities.Select(e => Users.Create(
                e.Id, e.Login, e.Name, e.HashPassword,
                e.Role, e.Balance, e.CreatedAt).Value);
        }

        public async Task<bool> VerifyAsync(string login, string password, CancellationToken token)
        {
            var user = await _context.UsersTable.FirstOrDefaultAsync(a => a.Login == login, token);
            if (user == null) return false;
            return Users.VerifyPassword(password, user.HashPassword);
        }

        public async Task<string> GetRoleAsync(string login, CancellationToken token)
        {
            var user = await _context.UsersTable.FirstOrDefaultAsync(a => a.Login == login, token);
            if (user is null) return string.Empty;
            return user.Role;
        }

        public async Task<Guid> GetIdAsync(string login, CancellationToken token)
        {
            var user = await _context.UsersTable.FirstOrDefaultAsync(a => a.Login == login, token);
            if (user is null) return Guid.Empty;
            return user.Id;
        }
    }
}