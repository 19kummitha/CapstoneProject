using CommunityConnect.Data;
using CommunityConnect.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CommunityConnect.Features.Admin.Queries.GetAllPostsQuery
{
    public class GetAllPostsQuery : IRequest<List<Post>>
    {
    }
    public class GetAllPostsQueryHandler : IRequestHandler<GetAllPostsQuery, List<Post>>
    {
        private readonly CommunityDbContext _context;

        public GetAllPostsQueryHandler(CommunityDbContext context)
        {
            _context = context;
        }
        public async Task<List<Post>> Handle(GetAllPostsQuery request, CancellationToken cancellationToken)
        {
            return await _context.Posts.ToListAsync(cancellationToken);
        }
    }
}
