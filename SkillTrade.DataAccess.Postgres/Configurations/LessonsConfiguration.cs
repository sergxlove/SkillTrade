using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SkillTrade.DataAccess.Postgres.Models;

namespace SkillTrade.DataAccess.Postgres.Configurations
{
    public class LessonsConfiguration : IEntityTypeConfiguration<LessonsEntity>
    {
        public void Configure(EntityTypeBuilder<LessonsEntity> builder)
        {
            throw new NotImplementedException();
        }
    }
}
