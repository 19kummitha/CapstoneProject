using Carter;
using MediatR;
using Microsoft.AspNetCore.Authorization;

namespace CommunityConnect.Features.Admin.Commands.DeletePostCommand
{
    public class DeletePostCommandEndPoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapDelete("/deletepost/{postId}", [Authorize(Roles = "Admin")] async (int postId, HttpRequest request, IMediator mediator) =>
            {
                var result = await mediator.Send(new DeletePostCommand { Id =  postId});

                if (result)
                {
                    return Results.Ok(new { Message = "Post deleted successfully" });
                }

                return Results.NotFound(new { Message = "Post not found" });
            })
            .WithName("DeletePost")
            .WithTags("Posts");
        }
    }
}
