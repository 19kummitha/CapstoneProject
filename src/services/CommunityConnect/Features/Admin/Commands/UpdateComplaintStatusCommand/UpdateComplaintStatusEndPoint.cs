using Carter;
using CommunityConnect.Data;
using CommunityConnect.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CommunityConnect.Features.Admin.Commands.UpdateComplaintStatusCommand
{
    public class UpdateComplaintStatusEndPoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPut("/complaints/{complaintId}/status", async (long complaintId, [FromBody] int status, IMediator mediator, [FromServices] IHttpContextAccessor httpContextAccessor, [FromServices] CommunityDbContext dbContext) =>
            {
                var user = httpContextAccessor.HttpContext.User;
                var role = user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;
                var residentId = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                // Restrict access for service providers
                if (role == "serviceprovider")
                {
                    return Results.Forbid();
                }

                // Check if the user is an admin or the owner of the complaint
                var complaint = await dbContext.Complaints.FindAsync(complaintId);

                if (complaint == null)
                {
                    return Results.NotFound(new { Message = "Complaint not found" });
                }

                // Allow update only if the user is admin or the owner of the complaint (resident)
                if (role == "Admin" || complaint.ResidentId == residentId)
                {
                    var command = new UpdateComplaintStatusCommand
                    {
                        ComplaintId = complaintId,
                        Status = status
                    };

                    var result = await mediator.Send(command);
                    return result ? Results.Ok(new { Message = "Complaint status updated successfully" }) : Results.StatusCode(500);
                }

                return Results.Forbid();
            })
             .WithName("UpdateComplaintStatus")
             .WithTags("Complaints");
        }

    }
}
