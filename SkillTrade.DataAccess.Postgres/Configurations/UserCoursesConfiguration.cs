using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SkillTrade.DataAccess.Postgres.Models;

namespace SkillTrade.DataAccess.Postgres.Configurations
{
    public class UserCoursesConfiguration : IEntityTypeConfiguration<UserCoursesEntity>
    {
        public void Configure(EntityTypeBuilder<UserCoursesEntity> builder)
        {
            throw new NotImplementedException();
        }
    }
}
