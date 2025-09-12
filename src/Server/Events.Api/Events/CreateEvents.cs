using Events.Api.Data;
using Events.Api._internal;
using Events.Api.Categories;
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


        public record CreateEventRequest(string Title, string Description, DateTime StartDate, DateTime EndDate, string Location, int[]? categories) { }


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
            var existingEvent = await dbContext.Events
                .FirstOrDefaultAsync(e => e.Title.ToLower() == request.Title.ToLower().Trim());

            if (existingEvent != null)
            {
                return TypedResults.BadRequest($"An Event with the title '{request.Title.Trim()}' already exists.");
            }

            // Validate and get categories if provided
            List<Category> categories = new();
            if (request.categories != null && request.categories.Any())
            {
                categories = await dbContext.Categories
                    .Where(c => request.categories.Contains(c.Id))
                    .ToListAsync();

                // Check if all requested categories exist
                var foundCategoryIds = categories.Select(c => c.Id).ToList();
                var missingCategoryIds = request.categories.Except(foundCategoryIds).ToList();

                if (missingCategoryIds.Any())
                {
                    return TypedResults.BadRequest($"Categories with IDs {string.Join(", ", missingCategoryIds)} do not exist.");
                }
            }

            var newEvent = new Event()
            {
                Title = request.Title.Trim(),
                Description = request.Description.Trim(),
                StartDate = DateTime.SpecifyKind(request.StartDate, DateTimeKind.Utc),
                EndDate = DateTime.SpecifyKind(request.EndDate, DateTimeKind.Utc),
                Location = request.Location,
                Categories = categories // Assign the categories to establish many-to-many relationship
            };
            dbContext.Events.Add(newEvent);
            await dbContext.SaveChangesAsync();
            return TypedResults.Ok(new CreateEventResponse(newEvent.Id));
        }
    }
}