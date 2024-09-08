using CommunityConnect.Data;
using CommunityConnect.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CommunityConnect.Features.Admin.Queries.GetEventById
{
    public class GetEventByIdQuery : IRequest<Event>
    {
        public int EventId { get; set; }
    }
    public class GetEventByIdQueryHandler : IRequestHandler<GetEventByIdQuery, Event>
    {
        private readonly CommunityDbContext _context;

        public GetEventByIdQueryHandler(CommunityDbContext context)
        {
            _context = context;
        }
        public async Task<Event> Handle(GetEventByIdQuery request, CancellationToken cancellationToken)
        {
            return await _context.Events
                .FirstOrDefaultAsync(e => e.EventId == request.EventId, cancellationToken);
        }
    }
}
