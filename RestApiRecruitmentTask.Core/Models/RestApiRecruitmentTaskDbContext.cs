using Microsoft.EntityFrameworkCore;

namespace RestApiRecruitmentTask.Core.Models
{
    public class RestApiRecruitmentTaskDbContext : DbContext
    {
        public DbSet<Tire> Tires { get; set; }
        public DbSet<Producer> Producers { get; set; }

        public RestApiRecruitmentTaskDbContext(DbContextOptions<RestApiRecruitmentTaskDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Tire>()
                .HasKey(t => t.Id);

            modelBuilder.Entity<Producer>()
                .HasKey(p => p.Id);

            base.OnModelCreating(modelBuilder);
        }
    }

}
