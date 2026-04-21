using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SkillTrade.DataAccess.Postgres.Models;

namespace SkillTrade.DataAccess.Postgres.Configurations
{
    public class CoursesConfiguration : IEntityTypeConfiguration<CoursesEntity>
    {
        public void Configure(EntityTypeBuilder<CoursesEntity> builder)
        {
            builder.ToTable("Courses");
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id)
                .IsRequired();
            builder.Property(c => c.IdActor)
                .IsRequired();
            builder.Property(c => c.Title)
                .IsRequired()
                .HasMaxLength(Core.Models.Courses.MAX_LENGTH_TITLE);
            builder.Property(c => c.Description)
                .IsRequired()
                .HasMaxLength(Core.Models.Courses.MAX_LENGTH_DESCRIPTION);
            builder.Property(c => c.Level)
                .IsRequired()
                .HasMaxLength(50);
            builder.Property(c => c.Price)
                .IsRequired()
                .HasPrecision(18, 2);
            builder.Property(c => c.LessonsCount)
                .IsRequired();
            builder.Property(c => c.DurationTimeHours)
                .IsRequired();
            builder.Property(c => c.CreatedAt)
                .IsRequired()
                .HasDefaultValueSql("CURRENT_TIMESTAMP");
            builder.HasIndex(c => c.IdActor);
            builder.HasIndex(c => c.Level);
            builder.HasIndex(c => c.Title);
        }
    }
}
