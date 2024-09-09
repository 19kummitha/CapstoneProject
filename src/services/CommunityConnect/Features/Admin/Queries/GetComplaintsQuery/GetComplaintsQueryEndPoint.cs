using Carter;
using MediatR;

namespace CommunityConnect.Features.Admin.Queries.GetComplaintsQuery
{
    public class GetComplaintsQueryEndPoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/admin/complaints/{residentId?}", async (string? residentId, IMediator mediator) =>
            {
                var result = await mediator.Send(new GetAllComplaintsQuery { ResidentId = residentId });
                return result != null ? Results.Ok(result) : Results.NotFound();
            })
            .RequireAuthorization("AdminOnly")
            .WithTags("Complaints");
        }
    }
}
