using Carter;
using MediatR;
using Microsoft.AspNetCore.Authorization;

namespace CommunityConnect.Features.Admin.Queries.GetAllServiceProvidersQuery
{
    public class GetAllServiceProviderEndpoint:ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/ServiceProvider", [Authorize(Roles = "Admin,User")] async (IMediator mediator) =>
            {
                var residents = await mediator.Send(new GetAllServiceProviderQuery());
                if (residents != null)
                {
                    return Results.Ok(residents);
                }
                return Results.StatusCode(500);
            })
            .WithName("GetAllServiceProviders")
            .WithTags("ServiceProviders");
        }
    }
}
