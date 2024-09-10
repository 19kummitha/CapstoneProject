using Carter;
using MediatR;
using Microsoft.AspNetCore.Authorization;

namespace CommunityConnect.Features.Resident.Command.CreateRequestService
{
    public class CreateRequestServiceEndPoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/service/request", [Authorize(Roles = "User,Admin")] async (CreateRequestCommand command, IMediator mediator) =>
            {
                var result = await mediator.Send(command);
                if (result)
                {
                    return Results.Ok(new { Message = "Service request created successfully" });
                }
                return Results.BadRequest(new { Message = "Failed to create service request" });
            })
        .WithName("CreateRequestService")
        .WithTags("Service Requests");
        }
    }
    }