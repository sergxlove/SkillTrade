using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SkillTrade.DataAccess.Postgres.Models;

namespace SkillTrade.DataAccess.Postgres.Configurations
{
    public class LessonsConfiguration : IEntityTypeConfiguration<LessonsEntity>
    {
        public void Configure(EntityTypeBuilder<LessonsEntity> builder)
        {
            builder.ToTable("Lessons");
            builder.HasKey(l => l.Id);
            builder.Property(l => l.Id)
                .IsRequired();
            builder.Property(l => l.IdCourse)
                .IsRequired();
            builder.Property(l => l.Title)
                .IsRequired()
                .HasMaxLength(Core.Models.Lessons.MAX_LENGTH_TITLE);
            builder.Property(l => l.Content)
                .IsRequired()
                .HasMaxLength(Core.Models.Lessons.MAX_LENGTH_CONTENT);
            builder.Property(l => l.CreatedAt)
                .IsRequired();
            builder.HasIndex(l => l.IdCourse);
            builder.HasIndex(l => l.Title);
        }
    }
}
