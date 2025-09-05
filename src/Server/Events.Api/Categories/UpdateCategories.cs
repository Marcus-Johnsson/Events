using Events.Api._internal;
using Events.Api.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticAssets;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;

namespace Events.Api.Categories
{
    public class UpdateCategories : IEndpoint
    {
        
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
                // Get category by ID
             app.MapPatch("/api/categories/{id:int}", Handle)
                .WithSummary("Update category Title")
                .Produces<Category>(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status404NotFound);
        }

        public record Request(string Title);

        public record Response(int Id);

        public static async Task<IResult> Handle(
            [FromBody] Request request,
            [FromServices] EventDbContext dbContext,
            int id)
        {
            // Check if title is null or empty
            if (string.IsNullOrWhiteSpace(request.Title))
            {
                return TypedResults.BadRequest("Title cannot be empty.");
            }

            // Check if a category with the same title already exists
            var existingCategory = await dbContext.Categories
                .FirstOrDefaultAsync(c => c.Title.ToLower() == request.Title.ToLower().Trim());

            if (existingCategory != null)
            {
                return TypedResults.BadRequest($"A category with the title '{request.Title.Trim()}' already exists.");
            }
            try
            {
                var oldCategory = await dbContext.Categories.FindAsync(id);
                oldCategory.Title = request.Title.Trim();


                dbContext.Update(oldCategory);
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
