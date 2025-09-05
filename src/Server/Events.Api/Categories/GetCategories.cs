using Events.Api.Data;
using Events.Api._internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Events.Api.Categories;

public class GetCategories : IEndpoint
{
    public static void MapEndpoint(IEndpointRouteBuilder app)
    {
        // Get all categories
        app.MapGet("/api/categories", HandleGetAll)
            .WithSummary("Get all categories")
            .Produces<List<Category>>(StatusCodes.Status200OK);

        // Get category by ID
        app.MapGet("/api/categories/{id:int}", HandleGetById)
            .WithSummary("Get category by ID")
            .Produces<Category>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound);
    }

    private static async Task<IResult> HandleGetAll(
        [FromServices] EventDbContext dbContext)
    {
        var categories = await dbContext.Categories.ToListAsync();
        return TypedResults.Ok(categories);
    }

    private static async Task<IResult> HandleGetById(
        int id,
        [FromServices] EventDbContext dbContext)
    {
        var category = await dbContext.Categories.FindAsync(id);
        return category is not null ? TypedResults.Ok(category) : TypedResults.NotFound();
    }
}
