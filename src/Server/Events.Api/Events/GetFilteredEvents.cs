using Events.Api._internal;
using Events.Api.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Events.Api.Events
{
    public class GetFilteredEvents : IEndpoint
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
           // Get event by ID
            app.MapGet("/api/events/{category}", Handle)
                .WithSummary("Get all events")
                .Produces<List<Event>>(StatusCodes.Status200OK);
        }

        private static async Task<IResult> Handle(
             string category,
             [FromServices] EventDbContext dbContext)

        {
            if (string.IsNullOrWhiteSpace(category))
            {
                return TypedResults.BadRequest("Id cannot be empty.");
            }

            var filteredEvents = await dbContext.Events
                .Include(c => c.Categories).Where(p => p.Categories.Any(c => c.Title == category)).ToListAsync();

            return filteredEvents.Any() ? TypedResults.Ok(filteredEvents) : TypedResults.NotFound();
        }


    }
}
