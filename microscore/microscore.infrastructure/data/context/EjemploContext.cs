using Microsoft.EntityFrameworkCore;

namespace microscore.infrastructure.data.context
{
    public partial class EjemploContext : DbContext
    {
        public EjemploContext(DbContextOptions<EjemploContext> options)
            : base(options)
        {
            this.ChangeTracker.LazyLoadingEnabled = false;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
