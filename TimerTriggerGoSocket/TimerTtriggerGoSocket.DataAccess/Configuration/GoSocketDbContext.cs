using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using TimerTtriggerGoSocket.DataAccess.Entity;

namespace TimerTtriggerGoSocket.DataAccess.Configuration
{
    public partial class GoSocketDbContext : DbContext
    {
        public IConfigurationRoot Configuration { get; set; }
        public GoSocketDbContext() { }

        public GoSocketDbContext(DbContextOptions<GoSocketDbContext> options)
            : base(options) { }

        public virtual DbSet<XmlFilesEntity> XmlFiles { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
                optionsBuilder.UseSqlServer(@"Data Source=localhost\SQLEXPRESS;Initial Catalog=GoSocket;Persist Security Info=True;User ID=avega;Password=f3l1p3v3g4;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Modern_Spanish_CI_AS");

            modelBuilder.Entity<XmlFilesEntity>(entity =>
            {
                entity.ToTable("XmlFiles");
                entity.Property(e => e.Id);
                entity.Property(e => e.Xml);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}