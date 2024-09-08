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
            app.MapGet("/events/{eventId}", [Authorize(Roles = "Admin")] async (int eventId, IMediator mediator) =>
            {
                var query = new GetEventByIdQuery { EventId = eventId };

                var result = await mediator.Send(query);

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
