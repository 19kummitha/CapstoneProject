using Carter;
using CommunityConnect.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;

namespace CommunityConnect.Features.Admin.Commands.DeleteEventCommand
{
    public class DeleteEventCommandEndPoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapDelete("/deleteevent/{eventId}", [Authorize(Roles = "Admin")] async (int eventId,HttpRequest request, IMediator mediator) =>
            {
                var result = await mediator.Send(new DeleteEventCommand { EventId = eventId });

                if (result)
                {
                    return Results.Ok(new { Message = "Event deleted successfully" });
                }

                return Results.NotFound(new { Message = "Event not found" });
            })
            .WithName("DeleteEvent")
            .WithTags("Events");
        }
    }
}
