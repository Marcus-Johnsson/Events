using Events.Api.Categories;

namespace Events.Api.Events
{
    public class Event
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string Title { get; set; }
        public string Location { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        
        // Many-to-many relationship with Categories
        public ICollection<Category>? Categories { get; set; }
    }
}
