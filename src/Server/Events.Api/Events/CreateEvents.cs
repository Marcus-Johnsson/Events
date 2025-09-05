using Events.Api.Data;
using Events.Api._internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace Events.Api.Events
{
    public class CreateEvents : IEndpoint
    {
        public static void MapEndpoint(IEndpointRouteBuilder app) => app
            .MapPost("/api/events", Handler)
            .WithSummary("Create event")
            .Produces<string>(StatusCodes.Status200OK)
            .Produces<string>(StatusCodes.Status400BadRequest)
            .Produces<string>(StatusCodes.Status500InternalServerError);

        
        public record CreateEventRequest(string Title, string Description, DateTime StartDate, DateTime EndDate, string Location) { }


        public record CreateEventResponse(int Id);

        public static async Task<IResult> Handler(
            [FromBody] CreateEventRequest request,
            [FromServices] EventDbContext dbContext)
        {
            if (string.IsNullOrWhiteSpace(request.Title))
            {
                return TypedResults.BadRequest("Title cannot be empty.");
            }
            if (string.IsNullOrWhiteSpace(request.Description))
            {
                return TypedResults.BadRequest("Description cannot be empty.");
            }

            // Check if a event with the same title already exists
            var existingCategory = await dbContext.Events
                .FirstOrDefaultAsync(c => c.Title.ToLower() == request.Title.ToLower().Trim());

            if (existingCategory != null)
            {
                return TypedResults.BadRequest($"A Event with the title '{request.Title.Trim()}' already exists.");
            }
            var newEvent = new Event()
            {
                Title = request.Title.Trim(),
                Description = request.Description.Trim(),
                StartDate = DateTime.SpecifyKind(request.StartDate, DateTimeKind.Utc),
                EndDate = DateTime.SpecifyKind(request.EndDate, DateTimeKind.Utc),
                Location = request.Location,
            };
            dbContext.Events.Add(newEvent);
            await dbContext.SaveChangesAsync();
            return TypedResults.Ok(new CreateEventResponse(newEvent.Id));
        }
    }
}
