

using Microsoft.EntityFrameworkCore;
using SinaiAPI.Models;

namespace SinaiAPI
{
    public class SinaiDbContext : DbContext
    {
        public SinaiDbContext(DbContextOptions<SinaiDbContext> options) : base(options) { }

        public DbSet<Department> Departments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Department>().ToTable("Departments");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            var connectionString = configuration.GetConnectionString("DefaultConnection");
            optionsBuilder.UseSqlServer(connectionString);
        }
    }
}
