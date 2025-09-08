using Events.Api._internal;
using Events.Api.Data;
using Events.Api.Migrations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace Events.Api.Events
{
    public class UpdateEvent : IEndpoint
    {
        public static void MapEndpoint(IEndpointRouteBuilder app) => app
            .MapPut("/api/events/{id}", Handler)
            .WithSummary("Update event")
            .Produces<string>(StatusCodes.Status200OK)
            .Produces<string>(StatusCodes.Status400BadRequest)
            .Produces<string>(StatusCodes.Status500InternalServerError);


        public record UpdateEventRequest(string Title, string Description, DateTime StartDate, DateTime EndDate, string Location) { }

        public record UpdateEventResponse(int Id);

        public static async Task<IResult> Handler(
            [FromBody] UpdateEventRequest request,
            [FromServices] EventDbContext dbContext,
             int id)
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

            try
            {
                var oldEvent = await dbContext.Events.FindAsync(id);
                oldEvent.Title = request.Title.Trim();
                oldEvent.Description = request.Description.Trim();
                oldEvent.StartDate = DateTime.SpecifyKind(request.StartDate, DateTimeKind.Utc);
                oldEvent.EndDate = DateTime.SpecifyKind(request.EndDate, DateTimeKind.Utc);
                oldEvent.Location = request.Location;

                await dbContext.SaveChangesAsync();
                return TypedResults.Ok(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return TypedResults.NotFound(id);
            }
        }
    }
}