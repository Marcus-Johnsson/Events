using Events.Api._internal;
using Events.Api.Data;
using Microsoft.AspNetCore.Mvc;

namespace Events.Api.Events
{
    public class DeleteEvents : IEndpoint
    {
        public static void MapEndpoint(IEndpointRouteBuilder app) => app
           .MapDelete("/api/events/{id:int}", Handle)
           .WithSummary("Delete event")
           .Produces(StatusCodes.Status204NoContent)
           .Produces(StatusCodes.Status404NotFound)
           .Produces<string>(StatusCodes.Status500InternalServerError);

        private static async Task<IResult> Handle(
            int id,
            [FromServices] EventDbContext myDb)
        {
            var deleteEvent = await myDb.Events.FindAsync(id);
            if (deleteEvent is null) return TypedResults.NotFound();

            myDb.Events.Remove(deleteEvent);
            await myDb.SaveChangesAsync();

            return TypedResults.NoContent();
        }
    }
}
