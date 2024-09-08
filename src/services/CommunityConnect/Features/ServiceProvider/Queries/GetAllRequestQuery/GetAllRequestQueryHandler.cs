using CommunityConnect.Data;
using CommunityConnect.DTO;
using CommunityConnect.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CommunityConnect.Features.ServiceProvider.Queries.GetAllRequestQuery
{
    public class GetAllServiceRequestsQuery : IRequest<List<RequestService>>
    {
        public string? ResidentId { get; set; }
    }
    public class GetAllRequestQueryHandler : IRequestHandler<GetAllServiceRequestsQuery, List<RequestService>>
    {
        private readonly CommunityDbContext _dbContext;

        public GetAllRequestQueryHandler(CommunityDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        

        public async Task<List<RequestService>> Handle(GetAllServiceRequestsQuery request, CancellationToken cancellationToken)
        {
            if (!string.IsNullOrEmpty(request.ResidentId))
            {
                return await _dbContext.RequestServices.Where(c => c.ResidentId == request.ResidentId).ToListAsync();
            }
            return await _dbContext.RequestServices.ToListAsync();
        }
    }
}
