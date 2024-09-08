using CommunityConnect.Data;
using CommunityConnect.Models;
using MediatR;
using System.Security.Claims;

namespace CommunityConnect.Features.Resident.Command.CreateRequestService
{
    public class CreateRequestCommand : IRequest<bool>
    {
        public string ResidentName { get; set; }
        public string ServiceType { get; set; }
        public string ServiceDescription { get; set; }
        public string FlatNo { get; set; }
    }
    public class CreateRequestServiceHandler : IRequestHandler<CreateRequestCommand, bool>
    {
        private readonly CommunityDbContext _dbContext;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CreateRequestServiceHandler(CommunityDbContext dbContext, IHttpContextAccessor httpContextAccessor)
        {
            _dbContext = dbContext;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<bool> Handle(CreateRequestCommand request, CancellationToken cancellationToken)
        {
            var residentId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(residentId))
                return false;

            var requestService = new RequestService
            {
                ResidentName = request.ResidentName,
                ServiceType = request.ServiceType,
                ServiceDescription = request.ServiceDescription,
                FlatNo = request.FlatNo,
                Status = "Requested",
                ResidentId = residentId
            };

            _dbContext.RequestServices.Add(requestService);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}
