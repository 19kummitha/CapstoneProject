using CommunityConnect.Data;
using CommunityConnect.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CommunityConnect.Features.Admin.Commands.UpdateComplaintStatusCommand
{
    public class UpdateComplaintStatusCommand : IRequest<bool>
    {
        public long ComplaintId { get; set; }
        public int Status { get; set; }
    }
    public class UpdateComplaintStatusHandler : IRequestHandler<UpdateComplaintStatusCommand, bool>
    {
        private readonly CommunityDbContext _context;

        public UpdateComplaintStatusHandler(CommunityDbContext context)
        {
            _context = context;
        }
        public async Task<bool> Handle(UpdateComplaintStatusCommand request, CancellationToken cancellationToken)
        {
            var complaint = await _context.Complaints.FirstOrDefaultAsync(c => c.ComplaintId == request.ComplaintId, cancellationToken);

            if (complaint == null)
            {
                // Complaint not found
                return false;
            }

            // Update the status only
            complaint.Status = (ComplaintStatus)request.Status;

            // Save the changes to the database
            await _context.SaveChangesAsync(cancellationToken);

            return true;
    }
    }
}
