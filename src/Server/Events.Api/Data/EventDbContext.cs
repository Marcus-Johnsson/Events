using Events.Api.Categories;
using Events.Api.Events;
using Microsoft.EntityFrameworkCore;

namespace Events.Api.Data
{
    public class EventDbContext : DbContext
    {
        public EventDbContext(DbContextOptions<EventDbContext> options) : base(options)
        {
        }

        public DbSet<Event> Events { get; set; }

        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure many-to-many relationship between Events and Categories
            modelBuilder.Entity<Event>()
                .HasMany(e => e.Categories)
                .WithMany(c => c.Events)
                .UsingEntity("EventCategories"); // Optional: specify join table name

            base.OnModelCreating(modelBuilder);
        }
    }
}
