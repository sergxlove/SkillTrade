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
            builder.HasData(
                new CoursesEntity
                {
                    Id = Guid.Parse("f1e2d3c4-b5a6-9780-1a2b-3c4d5e6f7a8b"),
                    IdActor = Guid.Parse("2c84949a-ebe9-4418-aa21-1d10f739e564"),
                    Title = "Fullstack JavaScript: от новичка до PRO",
                    Description = "Освой React, Node.js, MongoDB и создавай современные веб-приложения. Более 40 часов контента и 5 живых проектов.",
                    Level = "middle",
                    Price = 0,
                    LessonsCount = 3,
                    DurationTimeHours = 46,
                    CreatedAt = new DateTime(2026, 5, 3, 19, 3, 30, DateTimeKind.Utc)
                },
                new CoursesEntity
                {
                    Id = Guid.Parse("9a8b7c6d-5e4f-3a2b-1c0d-9e8f7a6b5c4d"),
                    IdActor = Guid.Parse("2c84949a-ebe9-4418-aa21-1d10f739e564"),
                    Title = "Python для Data Science и AI",
                    Description = "NumPy, Pandas, визуализация, основы машинного обучения. Итоговый проект — анализ данных.",
                    Level = "beginner",
                    Price = 0,
                    LessonsCount = 3,
                    DurationTimeHours = 28,
                    CreatedAt = new DateTime(2026, 5, 3, 19, 3, 30, DateTimeKind.Utc)
                },
                new CoursesEntity
                {
                    Id = Guid.NewGuid(),
                    IdActor = Guid.Parse("2c84949a-ebe9-4418-aa21-1d10f739e564"),
                    Title = "C# ASP.NET Core: разработка API и микросервисы",
                    Description = "Создание масштабируемых бэкенд-решений, Entity Framework, Docker, RabbitMQ.",
                    Level = "advanced",
                    Price = 0,
                    LessonsCount = 3,
                    DurationTimeHours = 52,
                    CreatedAt = new DateTime(2026, 5, 3, 19, 3, 30, DateTimeKind.Utc)
                },
                new CoursesEntity
                {
                    Id = Guid.NewGuid(),
                    IdActor = Guid.Parse("2c84949a-ebe9-4418-aa21-1d10f739e564"),
                    Title = "React + TypeScript: enterprise приложения",
                    Description = "Погружение в современный frontend: хуки, контекст, RTK Query, тестирование.",
                    Level = "middle",
                    Price = 0,
                    LessonsCount = 3,
                    DurationTimeHours = 34,
                    CreatedAt = new DateTime(2026, 5, 3, 19, 3, 30, DateTimeKind.Utc)
                });
        }
    }
}
