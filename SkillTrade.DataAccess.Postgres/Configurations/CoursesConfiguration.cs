using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SkillTrade.DataAccess.Postgres.Models;

namespace SkillTrade.DataAccess.Postgres.Configurations
{
    public class CoursesConfiguration : IEntityTypeConfiguration<CoursesEntity>
    {
        public void Configure(EntityTypeBuilder<CoursesEntity> builder)
        {
            throw new NotImplementedException();
        }
    }
}
