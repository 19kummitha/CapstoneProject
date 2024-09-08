using CommunityConnect.Data;
using CommunityConnect.Models;
using MediatR;
using System.Security.Claims;

namespace CommunityConnect.Features.Resident.Command.CreateComplaint
{
    public class CreateComplaintCommand:IRequest<bool>
    {
        public string PersonName { get; set; }
        public string FlatNo { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }
    public class CreateComplaintHandler : IRequestHandler<CreateComplaintCommand, bool>
    {
        private readonly CommunityDbContext _dbContext;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CreateComplaintHandler(CommunityDbContext dbContext, IHttpContextAccessor httpContextAccessor)
        {
            _dbContext = dbContext;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<bool> Handle(CreateComplaintCommand request, CancellationToken cancellationToken)
        {
            // Get ResidentId from JWT claims
            var residentId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(residentId))
                return false;

            var complaint = new Complaint
            {
                PersonName = request.PersonName,
                FlatNo = request.FlatNo,
                Title = request.Title,
                Description = request.Description,
                ResidentId = residentId  ,
                Status=ComplaintStatus.OPEN
            };

            _dbContext.Complaints.Add(complaint);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}
