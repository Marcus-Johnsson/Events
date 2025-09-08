using Events.Api.Events;
using System.Text.Json.Serialization;

namespace Events.Api.Categories;

public class Category
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;

    [JsonIgnore]
    // Many-to-many relationship with Events
    public ICollection<Event>? Events { get; set; }
}

