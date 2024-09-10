using Carter;
using MediatR;
using Microsoft.AspNetCore.Authorization;

namespace CommunityConnect.Features.Admin.Queries.GetAllPostsQuery
{
    public class GetAllPostsQueryEndPoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/getposts", [Authorize(Roles = "Admin,User,ServiceProvider")] async (IMediator mediator) =>
            {
                var posts = await mediator.Send(new GetAllPostsQuery());

                if (posts != null && posts.Count > 0)
                {
                    return Results.Ok(posts);
                }

                return Results.NotFound(new { Message = "No posts found" });
            })
            .WithName("GetAllPosts")
            .WithTags("Posts");
        }
    }
    }