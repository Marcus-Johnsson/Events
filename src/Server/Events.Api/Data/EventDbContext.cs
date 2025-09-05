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
    }
}
