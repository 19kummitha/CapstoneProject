using CommunityConnect.Models;
using MediatR;

namespace CommunityConnect.Features.Admin.Queries.GetEventById
{
    public class GetEventByIdQuery : IRequest<Event>
    {
        public int EventId { get; set; }
    }
    
    public class GetEventByIdQueryEndPoint
    {
    }
}
