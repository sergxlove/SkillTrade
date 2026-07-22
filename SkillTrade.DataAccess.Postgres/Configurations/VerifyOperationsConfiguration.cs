using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SkillTrade.DataAccess.Postgres.Models;

namespace SkillTrade.DataAccess.Postgres.Configurations
{
    public class VerifyOperationsConfiguration : IEntityTypeConfiguration<VerifyOperationsEntity>
    {
        public void Configure(EntityTypeBuilder<VerifyOperationsEntity> builder)
        {
            builder.ToTable("VerifyOperations");
            builder.HasKey(a => a.Id);
            builder.Property(a => a.Email)
                .IsRequired();
            builder.Property(a => a.Code)
                .IsRequired();
            builder.Property(a => a.DateCreate)
                .IsRequired();
            builder.Property(a => a.QuantityTry)
                .IsRequired();
            builder.HasIndex(a => a.Email);
        }
    }
}
