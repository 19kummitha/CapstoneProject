using Carter;
using CommunityConnect.Features.Resident.Command.CreateComplaint;
using MediatR;
using Microsoft.AspNetCore.Authorization;

namespace CommunityConnect.Features.Admin.Commands.AddEventCommand
{
    public class AddEventCommandEndPoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/createevent", [Authorize(Roles = "Admin")] async (CreateEventCommand command, IMediator mediator) =>
            {
                var result = await mediator.Send(command);
                if (result)
                {
                    return Results.Ok(new { Message = "Event added successfully" });
                }
                return Results.BadRequest(new { Message = "Failed to add Event" });
            })
            .WithName("AddEvent")
            .WithTags("Events");
        }
    }
}
