using Carter;
using MediatR;
using Microsoft.AspNetCore.Authorization;

namespace CommunityConnect.Features.ServiceProvider.Queries.GetAllRequestQuery
{
    public class GetAllRequestEndPoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/Service/requests/{residentId?}", [Authorize(Roles = "ServiceProvider")]  async (string? residentId, IMediator mediator) =>
            {
                var result = await mediator.Send(new GetAllServiceRequestsQuery { ResidentId = residentId });
                return result != null ? Results.Ok(result) : Results.NotFound();
            })
            .RequireAuthorization("ServiceProviderOnly")
            .WithTags("Requests");
        }

    }
}
