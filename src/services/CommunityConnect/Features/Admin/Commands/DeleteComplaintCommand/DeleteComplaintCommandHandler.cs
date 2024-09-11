using CommunityConnect.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CommunityConnect.Features.Admin.Commands.DeleteComplaintCommand
{
    public class DeleteComplaintCommand : IRequest<bool>
    {
        public long ComplaintId { get; set; }
    }
    public class DeleteComplaintCommandHandler:IRequestHandler<DeleteComplaintCommand,bool>
    {
        private readonly CommunityDbContext _dbContext;

        public DeleteComplaintCommandHandler(CommunityDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> Handle(DeleteComplaintCommand request, CancellationToken cancellationToken)
        {
            var complaint = await _dbContext.Complaints.FirstOrDefaultAsync(c => c.ComplaintId == request.ComplaintId, cancellationToken);

            if (complaint == null)
            {
                return false;
            }

            _dbContext.Complaints.Remove(complaint);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
   
}
