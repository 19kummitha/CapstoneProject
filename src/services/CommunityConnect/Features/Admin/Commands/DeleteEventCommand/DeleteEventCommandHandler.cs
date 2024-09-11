using CommunityConnect.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CommunityConnect.Features.Admin.Commands.DeleteEventCommand
{
    public class DeleteEventCommand : IRequest<bool>
    {
        public int EventId { get; set; }
    }
    public class DeleteEventCommandHandler:IRequestHandler<DeleteEventCommand,bool>
    {
        private readonly CommunityDbContext _context;

        public DeleteEventCommandHandler(CommunityDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(DeleteEventCommand request, CancellationToken cancellationToken)
        {
            var eventTodelete = await _context.Events.FirstOrDefaultAsync(c => c.EventId == request.EventId, cancellationToken);

            if (eventTodelete == null)
            {
                return false;
            }

            _context.Events.Remove(eventTodelete);
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}
