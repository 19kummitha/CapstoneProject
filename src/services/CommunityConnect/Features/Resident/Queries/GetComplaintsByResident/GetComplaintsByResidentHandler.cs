using CommunityConnect.Data;
using CommunityConnect.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace CommunityConnect.Features.Resident.Queries.GetComplaintsByResident
{
    public class GetComplaintsByResidentQuery : IRequest<List<Complaint>>
    {
    }
    public class GetComplaintsByResidentHandler : IRequestHandler<GetComplaintsByResidentQuery, List<Complaint>>
    {
        private readonly CommunityDbContext _dbContext;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public GetComplaintsByResidentHandler(CommunityDbContext dbContext, IHttpContextAccessor httpContextAccessor)
        {
            _dbContext = dbContext;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<List<Complaint>> Handle(GetComplaintsByResidentQuery request, CancellationToken cancellationToken)
        {
            var userId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            return await _dbContext.Complaints.Where(c => c.ResidentId == userId).ToListAsync();
        }

       
    }
}
