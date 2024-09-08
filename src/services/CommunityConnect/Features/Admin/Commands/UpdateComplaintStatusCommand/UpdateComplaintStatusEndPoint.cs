using Carter;
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
            app.MapPut("/complaints/{complaintId}/status", async (long complaintId, [FromBody] int status, IMediator mediator, [FromServices] IAuthorizationService authorizationService, HttpContext httpContext) =>
            {
                var user = httpContext.User;
                var role = user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;

                // Restrict access for service providers
                if (role == "serviceprovider")
                {
                    return Results.Forbid();
                }

                // Authorize user (Admin or regular user)
                var authorizationResult = await authorizationService.AuthorizeAsync(user, null, "UserOrAdmin");
                if (authorizationResult.Succeeded)
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
