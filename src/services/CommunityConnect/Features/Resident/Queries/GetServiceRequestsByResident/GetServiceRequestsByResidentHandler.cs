using CommunityConnect.Data;
using CommunityConnect.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace CommunityConnect.Features.Resident.Queries.GetServiceRequestsByResident
{
    public class GetServiceRequestsByResidentQuery : IRequest<List<RequestService>>
    {
    }
    public class GetServiceRequestsByResidentHandler : IRequestHandler<GetServiceRequestsByResidentQuery, List<RequestService>>
    {
        private readonly CommunityDbContext _dbContext;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public GetServiceRequestsByResidentHandler(CommunityDbContext dbContext, IHttpContextAccessor httpContextAccessor)
        {
            _dbContext = dbContext;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<List<RequestService>> Handle(GetServiceRequestsByResidentQuery request, CancellationToken cancellationToken)
        {
            var userId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            return await _dbContext.RequestServices.Where(c => c.ResidentId == userId).ToListAsync();
        }
    }
}
