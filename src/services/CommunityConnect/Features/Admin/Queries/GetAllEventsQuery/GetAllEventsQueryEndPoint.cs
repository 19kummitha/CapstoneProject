using Carter;
using MediatR;
using Microsoft.AspNetCore.Authorization;

namespace CommunityConnect.Features.Admin.Queries.GetAllEventsQuery
{
    public class GetAllEventsQueryEndPoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/getevents", [Authorize(Roles = "Admin,User")] async (IMediator mediator) =>
            {
                var events = await mediator.Send(new GetAllEventsQuery());

                if (events != null && events.Count > 0)
                {
                    return Results.Ok(events);
                }

                return Results.NotFound(new { Message = "No events found" });
            })
            .WithName("GetAllEvents")
            .WithTags("Events");
        }
    }
}
