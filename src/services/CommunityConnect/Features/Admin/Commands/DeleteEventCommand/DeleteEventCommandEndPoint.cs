using Carter;
using MediatR;
using Microsoft.AspNetCore.Authorization;

namespace CommunityConnect.Features.Admin.Commands.DeleteEventCommand
{
    public class DeleteEventCommandEndPoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapDelete("/events", [Authorize(Roles = "Admin")] async (DeleteEventCommand command, IMediator mediator) =>
            {
                var result = await mediator.Send(command);

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
