using Carter;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CommunityConnect.Features.Admin.Commands.AddAdminCommand
{
    public class AddAdminEndPoint:ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/register-admin", async ([FromBody] AddAdminCommand command, IMediator mediator) =>
            {
                var result = await mediator.Send(command);
                if (result)
                {
                    return Results.Ok(new { Message = "Admin Added successfully" });
                }
                return Results.StatusCode(500);
            })
            .WithName("RegisterAdmin")
            .WithTags("Admin");
        }
    }
}
