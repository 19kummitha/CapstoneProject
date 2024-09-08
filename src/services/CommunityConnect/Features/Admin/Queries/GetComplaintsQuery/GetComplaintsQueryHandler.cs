using CommunityConnect.Data;
using CommunityConnect.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CommunityConnect.Features.Admin.Queries.GetComplaintsQuery
{
    public class GetAllComplaintsQuery : IRequest<List<Complaint>>
    {
        public string? ResidentId { get; set; }
    }
    public class GetComplaintsQueryHandler : IRequestHandler<GetAllComplaintsQuery, List<Complaint>>
    {
        private readonly CommunityDbContext _dbContext;

        public GetComplaintsQueryHandler(CommunityDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<List<Complaint>> Handle(GetAllComplaintsQuery request, CancellationToken cancellationToken)
        {
            if (!string.IsNullOrEmpty(request.ResidentId))
            {
                return await _dbContext.Complaints.Where(c => c.ResidentId == request.ResidentId).ToListAsync();
            }

            return await _dbContext.Complaints.ToListAsync();
        }
    }
}
