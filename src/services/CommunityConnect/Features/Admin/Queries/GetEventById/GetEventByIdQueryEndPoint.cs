using Carter;
using CommunityConnect.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;

namespace CommunityConnect.Features.Admin.Queries.GetEventById
{


    public class GetEventByIdQueryEndPoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/events/get", [Authorize(Roles = "Admin,User")] async (HttpRequest request, IMediator mediator) =>
            {
                var command = await request.ReadFromJsonAsync<GetEventByIdQuery>();

                if (command == null || command.EventId <= 0)
                {
                    return Results.BadRequest(new { Message = "Invalid request data" });
                }

                var result = await mediator.Send(command);

                if (result != null)
                {
                    return Results.Ok(result);
                }

                return Results.NotFound(new { Message = "Event not found" });
            })
            .WithName("GetEventById")
            .WithTags("Events");
        }
    }
    }
