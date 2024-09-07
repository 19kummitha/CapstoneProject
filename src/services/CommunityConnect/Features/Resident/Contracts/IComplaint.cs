using CommunityConnect.Models;

namespace CommunityConnect.Features.Resident.Contracts
{
    public interface IComplaint
    {
        Task<Complaint> CreateComplaintAsync(Complaint complaint);
    }
}
