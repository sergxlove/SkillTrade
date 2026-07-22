using Microsoft.EntityFrameworkCore;
using SkillTrade.Core.Models;
using SkillTrade.DataAccess.Postgres.Abstractions;
using SkillTrade.DataAccess.Postgres.Models;

namespace SkillTrade.DataAccess.Postgres.Repositories
{
    public class VerifyOperationsRepository : IVerifyOperationsRepository
    {
        private readonly SkillTradeDbContext _context;
        public VerifyOperationsRepository(SkillTradeDbContext context)
        {
            _context = context;
        }

        public async Task<Guid> AddAsync(VerifyOperations oper, CancellationToken token)
        {
            VerifyOperationsEntity operEntity = new()
            {
                Id = oper.Id,
                Email = oper.Email,
                Code = oper.Code,
                DateCreate = oper.DateCreate,
                QuantityTry = oper.QuantityTry,
            };
            VerifyOperationsEntity? operFind = await _context.VerifyOperationsTable
                .FirstOrDefaultAsync(a => a.Email == operEntity.Email, token);
            if (operFind is null)
            {
                await _context.AddAsync(operEntity, token);
                await _context.SaveChangesAsync(token);
                return operEntity.Id;
            }
            else
            {
                int result = await _context.VerifyOperationsTable
                    .Where(a => a.Email == operEntity.Email)
                    .ExecuteUpdateAsync(a => a
                    .SetProperty(a => a.Code, operEntity.Code), token);
                if (result == 0)
                    return Guid.Empty;
                else
                    return operEntity.Id;
            }
        }

        public async Task<int> DeleteAsync(string email, CancellationToken token)
        {
            return await _context.VerifyOperationsTable
                .Where(a => a.Email == email)
                .ExecuteDeleteAsync(token);
        }

        public async Task<int> IncrementTryAsync(string email, CancellationToken token)
        {
            return await _context.VerifyOperationsTable
                .Where(a => a.Email == email)
                .ExecuteUpdateAsync(a => a
                .SetProperty(a => a.QuantityTry, a => a.QuantityTry + 1), token);
        }

        public async Task<bool> VerifyAsync(VerifyOperations oper, int minutesValid,
            CancellationToken token)
        {
            VerifyOperationsEntity? operFind = await _context.VerifyOperationsTable
                .FirstOrDefaultAsync(a => a.Email == oper.Email, token);
            if (operFind is null)
                return false;
            if (oper.Code != operFind.Code)
                return false;
            TimeSpan diff = oper.DateCreate - operFind.DateCreate;
            if (diff.Minutes > minutesValid)
                return false;
            else
                return true;
        }
    }
}
