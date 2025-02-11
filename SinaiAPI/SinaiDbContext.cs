

using Microsoft.EntityFrameworkCore;
using SinaiAPI.Models;

namespace SinaiAPI
{
    public class SinaiDbContext : DbContext
    {
        public SinaiDbContext(DbContextOptions<SinaiDbContext> options) : base(options) { }

        public DbSet<Department> Departments { get; set; }
        public DbSet<Guide> Guides { get; set; }
        public DbSet<Workplace> Workplaces { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Reservation> Reservations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Department>().ToTable("Departments");
            modelBuilder.Entity<Guide>().ToTable("Guides");
            modelBuilder.Entity<Workplace>()
                .ToTable("Workplaces")
                .HasOne(w => w.Department)
                .WithMany(d => d.Workplaces)
                .HasForeignKey(w => w.DepartmentId)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<User>().ToTable("Users");
            modelBuilder.Entity<Reservation>()
                .ToTable("Reservations")
                .HasOne(r => r.User)
                .WithMany(u => u.Reservations)
                .HasForeignKey(r => r.UserId)
                .OnDelete(DeleteBehavior.Cascade);
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
