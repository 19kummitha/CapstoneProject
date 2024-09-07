using Carter;
using CommunityConnect.DTO;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CommunityConnect.Features.Admin.Commands.AddResidentCommand
{
    
    public class AddResidentEndPoint:ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/register", async ([FromBody] AddResidentCommand command, IMediator mediator) =>
            {
                var result = await mediator.Send(command);
                if (result)
                {
                    return Results.Ok(new { Message = "Resident registered successfully" });
                }
                return Results.StatusCode(500);
            })
            .WithName("RegisterResident")
            .WithTags("Resident");
        }
    }

    }