using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SkillTrade.DataAccess.Postgres.Models;

namespace SkillTrade.DataAccess.Postgres.Configurations
{
    public class UserCoursesConfiguration : IEntityTypeConfiguration<UserCoursesEntity>
    {
        public void Configure(EntityTypeBuilder<UserCoursesEntity> builder)
        {
            builder.ToTable("UserCourses");
            builder.HasKey(uc => uc.Id);
            builder.Property(uc => uc.Id)
                .IsRequired();
            builder.Property(uc => uc.UserId)
                .IsRequired();
            builder.Property(uc => uc.CourseId)
                .IsRequired();
            builder.Property(uc => uc.CurrentProgress)
                .IsRequired()
                .HasDefaultValue(0);
            builder.Property(uc => uc.TotalProgress)
                .IsRequired();
            builder.Property(uc => uc.SubscribeTime)
                .IsRequired()
                .HasDefaultValueSql("CURRENT_TIMESTAMP");
            builder.HasIndex(uc => new { uc.UserId, uc.CourseId })
                .IsUnique();
            builder.HasIndex(uc => uc.UserId);
            builder.HasIndex(uc => uc.CourseId);
        }
    }
}
