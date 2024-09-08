using Carter;
using MediatR;
using Microsoft.AspNetCore.Authorization;

namespace CommunityConnect.Features.Admin.Commands.UpdateEventCommand
{
    public class UpdateEventCommandEndPoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPut("/events", [Authorize(Roles = "Admin")] async (UpdateEventCommand command, IMediator mediator) =>
            {
                var result = await mediator.Send(command);

                if (result)
                {
                    return Results.Ok(new { Message = "Event updated successfully" });
                }

                return Results.NotFound(new { Message = "Event not found" });
            })
            .WithName("UpdateEvent")
            .WithTags("Events");
        }
    }
}
