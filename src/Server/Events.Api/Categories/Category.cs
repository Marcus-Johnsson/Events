using Events.Api.Events;

namespace Events.Api.Categories;

public class Category
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    
    // Many-to-many relationship with Events
    public ICollection<Event>? Events { get; set; }
}

