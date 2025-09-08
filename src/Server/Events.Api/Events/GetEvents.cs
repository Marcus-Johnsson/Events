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
            app.MapGet("/api/events", Handle)
                .WithSummary("Get all events")
                .Produces<List<Event>>(StatusCodes.Status200OK);
        }

        private static async Task<IResult> Handle(
            [FromServices] EventDbContext dbContext)
        {
            var assignedCategories = await dbContext.Events
                .Include(c => c.Categories)
                .ToListAsync();
            return TypedResults.Ok(assignedCategories);
        }

    }
}
