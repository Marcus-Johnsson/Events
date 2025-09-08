using Events.Api._internal;
using Events.Api.Data;
using Events.Api.Categories;
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


        public record UpdateEventRequest(string Title, string Description, DateTime StartDate, DateTime EndDate, string Location, int[]? CategoryIds) { }

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
                // Load the existing event including its categories
                var oldEvent = await dbContext.Events
                    .Include(e => e.Categories)
                    .FirstOrDefaultAsync(e => e.Id == id);
                
                if (oldEvent == null)
                {
                    return TypedResults.NotFound($"Event with ID {id} not found.");
                }

                // Update basic properties
                oldEvent.Title = request.Title.Trim();
                oldEvent.Description = request.Description.Trim();
                oldEvent.StartDate = DateTime.SpecifyKind(request.StartDate, DateTimeKind.Utc);
                oldEvent.EndDate = DateTime.SpecifyKind(request.EndDate, DateTimeKind.Utc);
                oldEvent.Location = request.Location;

                // Handle categories update
                if (request.CategoryIds != null)
                {
                    // Get the new categories for the database
                    var newCategories = await dbContext.Categories
                        .Where(c => request.CategoryIds.Contains(c.Id))
                        .ToListAsync();

                    // Check if all requested categories exist
                    var foundCategoryIds = newCategories.Select(c => c.Id).ToList();
                    var missingCategoryIds = request.CategoryIds.Except(foundCategoryIds).ToList();

                    if (missingCategoryIds.Any())
                    {
                        return TypedResults.BadRequest($"Categories with IDs {string.Join(", ", missingCategoryIds)} do not exist.");
                    }

                    oldEvent.Categories.Clear();
                    foreach (var category in newCategories)
                    {
                        oldEvent.Categories.Add(category);
                    }
                }

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