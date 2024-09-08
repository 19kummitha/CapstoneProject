using Carter;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace CommunityConnect.Features.Resident.Command.CreateComplaint
{
    public class CreateComplaintEndPoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/complaint", [Authorize(Roles ="User")] async (CreateComplaintCommand command, IMediator mediator) =>
            {
                var result = await mediator.Send(command);
                if (result)
                {
                    return Results.Ok(new { Message = "Complaint added successfully" });
                }
                return Results.BadRequest(new { Message = "Failed to add complaint" });
            })
            .WithName("CreateComplaint")
            .WithTags("Complaints");
        }
    }
}
