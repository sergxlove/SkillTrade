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

        public async Task<Users?> GetByIdAsync(Guid id)
        {
            var entity = await _context.UsersTable
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Id == id);

            if (entity == null) return null;

            return Users.Create(
                entity.Id, entity.Login, entity.Name, entity.HashPassword,
                entity.Role, entity.Balance, entity.CreatedAt).Value;
        }

        public async Task<Users?> GetByLoginAsync(string login)
        {
            var entity = await _context.UsersTable
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Login.ToLower() == login.ToLower());

            if (entity == null) return null;

            return Users.Create(
                entity.Id, entity.Login, entity.Name, entity.HashPassword,
                entity.Role, entity.Balance, entity.CreatedAt).Value;
        }

        public async Task<IEnumerable<Users>> GetAllAsync()
        {
            var entities = await _context.UsersTable
                .AsNoTracking()
                .OrderByDescending(u => u.CreatedAt)
                .ToListAsync();

            return entities.Select(e => Users.Create(
                e.Id, e.Login, e.Name, e.HashPassword,
                e.Role, e.Balance, e.CreatedAt).Value);
        }

        public async Task<IEnumerable<Users>> GetByRoleAsync(string role)
        {
            var entities = await _context.UsersTable
                .AsNoTracking()
                .Where(u => u.Role.ToLower() == role.ToLower())
                .OrderByDescending(u => u.CreatedAt)
                .ToListAsync();

            return entities.Select(e => Users.Create(
                e.Id, e.Login, e.Name, e.HashPassword,
                e.Role, e.Balance, e.CreatedAt).Value);
        }

        public async Task<Guid> CreateAsync(Users user)
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

            await _context.UsersTable.AddAsync(entity);
            await _context.SaveChangesAsync();

            return entity.Id;
        }

        public async Task UpdateAsync(Users user)
        {
            var entity = await _context.UsersTable.FindAsync(user.Id);
            if (entity != null)
            {
                entity.Login = user.Login;
                entity.Name = user.Name;
                entity.Role = user.Role;

                _context.UsersTable.Update(entity);
                await _context.SaveChangesAsync();
            }
        }

        public async Task UpdateBalanceAsync(Guid userId, decimal newBalance)
        {
            var entity = await _context.UsersTable.FindAsync(userId);
            if (entity != null)
            {
                entity.Balance = newBalance;
                _context.UsersTable.Update(entity);
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteAsync(Guid id)
        {
            var entity = await _context.UsersTable.FindAsync(id);
            if (entity != null)
            {
                _context.UsersTable.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> ExistsByLoginAsync(string login)
        {
            return await _context.UsersTable
                .AnyAsync(u => u.Login.ToLower() == login.ToLower());
        }

        public async Task<bool> ExistsByIdAsync(Guid id)
        {
            return await _context.UsersTable.AnyAsync(u => u.Id == id);
        }

        public async Task<IEnumerable<Users>> GetPagedAsync(int page, int pageSize)
        {
            var entities = await _context.UsersTable
                .AsNoTracking()
                .OrderByDescending(u => u.CreatedAt)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return entities.Select(e => Users.Create(
                e.Id, e.Login, e.Name, e.HashPassword,
                e.Role, e.Balance, e.CreatedAt).Value);
        }
    }
}
