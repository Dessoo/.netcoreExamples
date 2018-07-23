using Microsoft.EntityFrameworkCore;

namespace DataAccess.Models
{
    public partial class TestContext : DbContext
    {
        public TestContext(DbContextOptions<TestContext> options)
            : base(options)
        {
            //settings EF
            //this.Database.SetCommandTimeout
        }

        public virtual DbSet<User> User { get; set; }

        public virtual DbSet<EventLog> EventLog { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.FirstName).HasMaxLength(50);

                entity.Property(e => e.LastName).HasMaxLength(50);
            });
            //naming convetions ?
            modelBuilder.Entity<EventLog>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("Id");

                entity.Property(e => e.EventId).HasColumnName("EventId");

                entity.Property(e => e.LogLevel).HasMaxLength(50).IsRequired();

                entity.Property(e => e.Message).HasMaxLength(4000);

                entity.Property(e => e.CreatedDateTime).IsRequired();

                entity.Property(e => e.Host).IsRequired();

                entity.Property(e => e.Logger).IsRequired();
            });
        }
    }
}
