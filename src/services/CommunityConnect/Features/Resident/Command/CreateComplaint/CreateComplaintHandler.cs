using CommunityConnect.Features.Resident.Contracts;
using CommunityConnect.Models;
using MediatR;

namespace CommunityConnect.Features.Resident.Command.CreateComplaint
{
    public class CreateComplaintCommand:IRequest<bool>
    {
        public string PersonName { get; set; }
        public string FlatNo { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public long ResidentId { get; set; }
    }
    public class CreateComplaintHandler : IRequestHandler<CreateComplaintCommand, bool>
    {
        private readonly IComplaint _complaintService;

        public CreateComplaintHandler(IComplaint complaintService)
        {
            _complaintService = complaintService;
        }

        public async Task<bool> Handle(CreateComplaintCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await _complaintService.CreateComplaintAsync(new Complaint
                {
                    PersonName = request.PersonName,
                    FlatNo = request.FlatNo,
                    Title = request.Title,
                    Description = request.Description,
                    ResidentId = request.ResidentId
                });

                return true;
            }
            catch
            {
                // Log exception or handle error as needed
                return false;
            }
        }
    }
}
