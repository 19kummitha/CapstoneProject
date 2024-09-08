using Carter;
using MediatR;
using System.Security.Claims;

namespace CommunityConnect.Features.Admin.Commands.DeleteComplaintCommand
{

    public class DeleteComplaintEndPoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapDelete("/complaints/{complaintId}", async (long complaintId, IMediator mediator, HttpContext httpContext) =>
            {
                // Extract user role from claims
                var role = httpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;

                // Check if the role is either Admin or User
                if (role == "Admin" || role == "User")
                {
                    var result = await mediator.Send(new DeleteComplaintCommand { ComplaintId = complaintId });

                    if (result)
                    {
                        return Results.Ok(new { Message = "Complaint deleted successfully" });
                    }

                    return Results.NotFound(new { Message = "Complaint not found" });
                }

                return Results.Forbid();
            })
            .WithName("DeleteComplaint")
            .WithTags("Complaints");
        }
    }
}
