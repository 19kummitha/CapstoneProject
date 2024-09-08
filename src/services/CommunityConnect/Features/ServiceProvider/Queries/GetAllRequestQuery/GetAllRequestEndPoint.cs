using Carter;
using MediatR;

namespace CommunityConnect.Features.ServiceProvider.Queries.GetAllRequestQuery
{
    public class GetAllRequestEndPoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/Service/requests/{residentId}", async (string? residentId, IMediator mediator) =>
            {
                var result = await mediator.Send(new GetAllServiceRequestsQuery { ResidentId = residentId });
                return result != null ? Results.Ok(result) : Results.NotFound();
            })
            .RequireAuthorization("ServiceProviderOnly")
            .WithTags("Requests");
        }

    }
}
