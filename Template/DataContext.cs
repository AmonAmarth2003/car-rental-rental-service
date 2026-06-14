using Microsoft.EntityFrameworkCore;
using Template.Entities;

namespace Template.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<Rental> Rentals { get; set; }
    
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Rental>().ToTable("Rentals");

            base.OnModelCreating(modelBuilder);
        }
    }
}
