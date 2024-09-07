using CommunityConnect.Data;
using CommunityConnect.Features.Resident.Contracts;
using CommunityConnect.Models;

namespace CommunityConnect.Features.Resident.Services
{
    public class ComplaintService:IComplaint
    {
        private readonly CommunityDbContext _context;

        public ComplaintService(CommunityDbContext context)
        {
            _context = context;
        }

        public async Task<Complaint> CreateComplaintAsync(Complaint complaint)
        {
            _context.Complaints.Add(complaint);
            await _context.SaveChangesAsync();
            return complaint;
        }
    }
}
