using Carter;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace CommunityConnect.Features.Resident.Command.CreateComplaint
{
    public class CreateComplaintEndPoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/create-complaint", async (HttpContext httpContext, IMediator mediator) =>
            {
                var command = await httpContext.Request.ReadFromJsonAsync<CreateComplaintCommand>();

                if (command == null)
                {
                    return Results.BadRequest("Invalid complaint data.");
                }

                var result = await mediator.Send(command);

                if (result)
                {
                    return Results.Ok(new { Message = "Complaint created successfully." });
                }

                return Results.Problem("An error occurred while creating the complaint."); // Use Results.Problem for error messages
            })
            .WithName("CreateComplaint")
            .WithTags("Complaint");
        }
    }
}
