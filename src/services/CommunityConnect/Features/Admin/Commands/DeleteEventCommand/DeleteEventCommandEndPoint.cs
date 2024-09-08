using Carter;
using MediatR;
using Microsoft.AspNetCore.Authorization;

namespace CommunityConnect.Features.Admin.Commands.DeleteEventCommand
{
    public class DeleteEventCommandEndPoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapDelete("/deleteevent", [Authorize(Roles = "Admin")] async (HttpRequest request, IMediator mediator) =>
            {
                var command = await request.ReadFromJsonAsync<DeleteEventCommand>();

                if (command == null || command.EventId <= 0)
                {
                    return Results.BadRequest(new { Message = "Invalid request data" });
                }

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
