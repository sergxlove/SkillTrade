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
        }
    }
}
