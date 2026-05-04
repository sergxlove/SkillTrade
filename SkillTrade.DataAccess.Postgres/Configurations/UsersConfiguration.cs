using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SkillTrade.DataAccess.Postgres.Models;

namespace SkillTrade.DataAccess.Postgres.Configurations
{
    public class UsersConfiguration : IEntityTypeConfiguration<UsersEntity>
    {
        public void Configure(EntityTypeBuilder<UsersEntity> builder)
        {
            builder.ToTable("Users");
            builder.HasKey(u => u.Id);
            builder.Property(u => u.Id)
                .IsRequired();
            builder.Property(u => u.Login)
                .IsRequired()
                .HasMaxLength(Core.Models.Users.MAX_LENGTH_LOGIN);
            builder.Property(u => u.Name)
                .IsRequired()
                .HasMaxLength(Core.Models.Users.MAX_LENGTH_NAME);
            builder.Property(u => u.HashPassword)
                .IsRequired()
                .HasMaxLength(255);
            builder.Property(u => u.Role)
                .IsRequired()
                .HasMaxLength(20);
            builder.Property(u => u.Balance)
                .IsRequired()
                .HasPrecision(18, 2)
                .HasDefaultValue(0);
            builder.Property(u => u.CreatedAt)
                .IsRequired();
            builder.HasIndex(u => u.Login)
                .IsUnique();
            builder.HasIndex(u => u.Role);
            builder.HasData(
                new UsersEntity
                {
                    Id = Guid.Parse("f07bbc78-67d1-452e-8bad-21fc146b56e3"),
                    Login = "student123",
                    Name = "student",
                    HashPassword = "$2a$11$TqnnLCGo8zSb2z9hVzpwSetLTFxczO1ASG6f6Mn7ap7VVi4MPztbO",
                    Role = "student",
                    Balance = 0.00m,
                    CreatedAt = new DateTime(2026, 5, 3, 19, 3, 30, DateTimeKind.Utc)
                },
                new UsersEntity
                {
                    Id = Guid.Parse("2c84949a-ebe9-4418-aa21-1d10f739e564"),
                    Login = "actor123",
                    Name = "actor",
                    HashPassword = "$2a$11$KkcWYSsG4a.G3Ax9vpK7f.VpR.AD8rp1P/tOBhMUityHntW366ZH.",
                    Role = "actor",
                    Balance = 0.00m,
                    CreatedAt = new DateTime(2026, 5, 3, 19, 3, 30, DateTimeKind.Utc)
                },
                new UsersEntity
                {
                    Id = Guid.Parse("5d03765b-47d6-4f5b-9bd8-3d41bd2f62a3"),
                    Login = "admin123",
                    Name = "admin",
                    HashPassword = "$2a$11$TPVCYIUVQQ1H5DTLLjcRDOBGzPe4cAJ/rod66GzgGhrSEOFSz2k/O",
                    Role = "admin",
                    Balance = 0.00m,
                    CreatedAt = new DateTime(2026, 5, 3, 19, 3, 30, DateTimeKind.Utc)
                }
                );
        }
    }
}
