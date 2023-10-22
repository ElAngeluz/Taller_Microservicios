using microscore.domain.entities.Accounts;
using microscore.domain.entities.People;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Logging;

namespace microscore.adapters.context
{
    public partial class MicrosContext : DbContext
    {
        public DbSet<Account> Account { get; set; }
        public DbSet<Client> Client { get; set; }
        public DbSet<Movement> Movement { get; set; }
        public DbSet<Person> Person { get; set; }

        public MicrosContext(DbContextOptions<MicrosContext> options) : base(options)
        {
            ChangeTracker.LazyLoadingEnabled = false;
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }

    public class MicrosContextFactory : IDesignTimeDbContextFactory<MicrosContext>
    {
        public MicrosContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<MicrosContext>();
            optionsBuilder.UseSqlServer("Server=localhost,1433;Database=micros;User Id=sa;Password=Passw0rd123@;");

            return new MicrosContext(optionsBuilder.Options);
        }
    }
}