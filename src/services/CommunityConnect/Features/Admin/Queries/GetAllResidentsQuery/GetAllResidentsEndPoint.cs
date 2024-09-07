using Carter;
using MediatR;
using Microsoft.AspNetCore.Authorization;

namespace CommunityConnect.Features.Admin.Queries.GetAllResidentsQuery
{
    public class GetAllResidentsEndPoint:ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/resident", [Authorize(Roles = "Admin")] async (IMediator mediator) =>
            {
                var residents = await mediator.Send(new GetAllResidentsQuery());
                if (residents != null)
                {
                    return Results.Ok(residents);
                }
                return Results.StatusCode(500);
            })
            .WithName("GetAllResidents")
            .WithTags("Residents");
        }
    }
}
