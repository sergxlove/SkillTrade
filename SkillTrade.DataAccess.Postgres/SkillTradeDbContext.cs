using Microsoft.EntityFrameworkCore;
using SkillTrade.DataAccess.Postgres.Configurations;
using SkillTrade.DataAccess.Postgres.Models;

namespace SkillTrade.DataAccess.Postgres
{
    public class SkillTradeDbContext : DbContext
    {
        public SkillTradeDbContext(DbContextOptions<SkillTradeDbContext> options) 
            : base(options) { }

        public DbSet<CoursesEntity> CoursesTable { get; set; }
        public DbSet<LessonsEntity> LessonsTable { get; set; }
        public DbSet<UserCoursesEntity> UserCoursesTable { get; set; }
        public DbSet<UsersEntity> UsersTable { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CoursesConfiguration());
            modelBuilder.ApplyConfiguration(new LessonsConfiguration());
            modelBuilder.ApplyConfiguration(new UserCoursesConfiguration());
            modelBuilder.ApplyConfiguration(new UsersConfiguration());
            base.OnModelCreating(modelBuilder);
        }
    }
}
