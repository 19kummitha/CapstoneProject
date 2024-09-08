using Carter;
using MediatR;

namespace CommunityConnect.Features.Resident.Queries.GetServiceRequestsByResident
{
    public class GetServiceRequestsByResidentEndPoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/resident/requestService", async (IMediator mediator) =>
            {
                var result = await mediator.Send(new GetServiceRequestsByResidentQuery());
                return result != null ? Results.Ok(result) : Results.NotFound();
            })
             .RequireAuthorization("UserOnly")
             .WithTags("Complaints");
        }
    }
}
