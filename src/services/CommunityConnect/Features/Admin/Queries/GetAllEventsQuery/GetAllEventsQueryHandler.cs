using CommunityConnect.Data;
using CommunityConnect.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CommunityConnect.Features.Admin.Queries.GetAllEventsQuery
{
    public class GetAllEventsQuery : IRequest<List<Event>>
    {
    }

    public class GetAllEventsQueryHandler:IRequestHandler<GetAllEventsQuery,List<Event>>
    {
        private readonly CommunityDbContext _context;

        public GetAllEventsQueryHandler(CommunityDbContext context)
        {
            _context = context;
        }

        public async Task<List<Event>> Handle(GetAllEventsQuery request, CancellationToken cancellationToken)
        {
            return await _context.Events.ToListAsync(cancellationToken);
        }
    }
}
