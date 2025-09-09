using Events.Api.Data;
using Events.Api._internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Events.Api.Categories;

public class CreateCategories : IEndpoint
{
    public static void MapEndpoint(IEndpointRouteBuilder app) => app
        .MapPost("/api/categories", Handle)
        .WithSummary("Create category")
        .Produces<Response>(StatusCodes.Status200OK)
        .Produces<string>(StatusCodes.Status400BadRequest)
        .Produces<string>(StatusCodes.Status500InternalServerError);

    public record Response(int Id);

    private static async Task<IResult> Handle(
        [FromBody] string title,
        [FromServices] EventDbContext dbContext)
    {
        // Check if title is null or empty
        if (string.IsNullOrWhiteSpace(title))
        {
            return TypedResults.BadRequest("Title cannot be empty.");
        }

        // Check if a category with the same title already exists
        var existingCategory = await dbContext.Categories
            .FirstOrDefaultAsync(c => c.Title.ToLower() == title.ToLower().Trim());

        if (existingCategory != null)
        {
            return TypedResults.BadRequest($"A category with the title '{title.Trim()}' already exists.");
        }

        var category = new Category
        {
            Title = title.Trim()
        };

        dbContext.Categories.Add(category);
        await dbContext.SaveChangesAsync();

        return TypedResults.Ok(new Response(category.Id));
    }
}
