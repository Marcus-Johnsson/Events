using Events.Api.Data;
using Events.Api._internal;
using Microsoft.AspNetCore.Mvc;

namespace Events.Api.Categories
{
    public class DeleteCategories : IEndpoint
    {
        public static void MapEndpoint(IEndpointRouteBuilder app) => app
            .MapDelete("/api/categories/{id:int}", Handle)
            .WithSummary("Delete category")
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status404NotFound)
            .Produces<string>(StatusCodes.Status500InternalServerError);

        private static async Task<IResult> Handle(
            int id,
            [FromServices] EventDbContext myDb)
        {
            var deleteCategory = await myDb.Categories.FindAsync(id);
            if (deleteCategory is null) return TypedResults.NotFound();

            myDb.Categories.Remove(deleteCategory);
            await myDb.SaveChangesAsync();

            return TypedResults.NoContent();
        }
    }
}