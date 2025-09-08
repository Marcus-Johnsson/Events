using Events.Api._internal;
using Events.Api.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Events.Api.Events
{
    public class GetEvents : IEndpoint
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            // Get all events
            app.MapGet("/api/events", HandleGetAll)
                .WithSummary("Get all events")
                .Produces<List<Event>>(StatusCodes.Status200OK);

            // Get event by ID
            app.MapGet("/api/events/{ids}", HandleGetById)
                .WithSummary("Get events by ID")
                .Produces<List<Event>>(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status404NotFound);
        }

        private static async Task<IResult> HandleGetAll(
            [FromServices] EventDbContext dbContext)
        {
            var events = await dbContext.Events.ToListAsync();
            return TypedResults.Ok(events);
        }

        private static async Task<IResult> HandleGetById(
            string ids,
            [FromServices] EventDbContext dbContext)

        {
            if (string.IsNullOrWhiteSpace(ids))
            {
                return TypedResults.BadRequest("Id cannot be empty.");
            }

            var assignedCategories = await dbContext.Events
                .Include(c => c.Categories)
                .FirstOrDefaultAsync();

            var idArray = ids.Split(',').Select(int.Parse).ToArray();

            var events = await dbContext.Events
                .Where(e => idArray.Contains(e.Id))
                .ToListAsync();

            return events.Any() ? TypedResults.Ok(events) : TypedResults.NotFound();
        }
    }
}
