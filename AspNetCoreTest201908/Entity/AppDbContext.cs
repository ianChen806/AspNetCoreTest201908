using Microsoft.EntityFrameworkCore;

namespace AspNetCoreTest201908.Entity
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Profile> Profile { get; set; }
        public DbQuery<VProfile> VProfile { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Query<VProfile>().ToView("VProfile");
        }
    }

    public class VProfile
    {
        public string Name { get; set; }
    }
}