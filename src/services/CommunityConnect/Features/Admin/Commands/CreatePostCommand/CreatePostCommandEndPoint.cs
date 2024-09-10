using Carter;
using MediatR;
using Microsoft.AspNetCore.Authorization;

namespace CommunityConnect.Features.Admin.Commands.CreatePostCommand
{
    public class CreatePostCommandEndPoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/createpost", [Authorize(Roles = "Admin")] async (HttpContext httpContext, IMediator mediator) =>
            {
                var form = httpContext.Request.Form;
                var command = new CreatePostCommand
                {
                    Title = form["Title"],
                    Content = form["Content"],
                    Media = form.Files.FirstOrDefault() // Get the uploaded file
                };

                var result = await mediator.Send(command);
                if (result)
                {
                    return Results.Ok(new { Message = "Post created successfully" });
                }
                return Results.BadRequest(new { Message = "Failed to create post" });
            })
            .WithName("CreatePost")
            .WithTags("Posts");
        }
    }
}