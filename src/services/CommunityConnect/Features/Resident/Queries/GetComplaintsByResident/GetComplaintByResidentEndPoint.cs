using Carter;
using MediatR;

namespace CommunityConnect.Features.Resident.Queries.GetComplaintsByResident
{
    public class GetComplaintByResidentEndPoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/resident/complaints", async (IMediator mediator) =>
            {
                var result = await mediator.Send(new GetComplaintsByResidentQuery());
                return result != null ? Results.Ok(result) : Results.NotFound();
            })
             .RequireAuthorization("UserOnly")
             .WithTags("Complaints");
        }
    }

}
