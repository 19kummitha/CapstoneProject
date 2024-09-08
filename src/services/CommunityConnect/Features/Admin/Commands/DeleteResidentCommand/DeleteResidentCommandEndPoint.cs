using Carter;
using MediatR;
using Microsoft.AspNetCore.Authorization;

namespace CommunityConnect.Features.Admin.Commands.DeleteResidentCommand
{
    public class DeleteResidentCommandEndPoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapDelete("/resident/{residentId}", [Authorize(Roles = "Admin")] async (string residentId, IMediator mediator) =>
            {
                var result = await mediator.Send(new DeleteResidentCommand { ResidentId = residentId });

                if (result)
                {
                    return Results.Ok(result);
                }

                return Results.StatusCode(500);
            })
            .WithName("DeleteResident")
            .WithTags("Residents");
        }
    }
}
