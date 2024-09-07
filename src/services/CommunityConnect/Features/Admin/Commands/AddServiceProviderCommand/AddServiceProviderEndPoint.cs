using Carter;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CommunityConnect.Features.Admin.Commands.AddServiceProviderCommand
{
    public class AddServiceProviderEndPoint:ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/register-service", async ([FromBody] AddServiceProviderCommand command, IMediator mediator) =>
            {
                var result = await mediator.Send(command);
                if (result)
                {
                    return Results.Ok(new { Message = "ServiceProvider registered successfully" });
                }
                return Results.StatusCode(500);
            })
            .WithName("RegisterServiceProvider")
            .WithTags("ServiceProvider");
        }
    }
}
