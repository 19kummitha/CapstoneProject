using CommunityConnect.Data;
using MediatR;

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
            var eventToDelete = await _context.Events.FindAsync(request.EventId);

            if (eventToDelete == null)
            {
                return false;
            }

            _context.Events.Remove(eventToDelete);
            var result = await _context.SaveChangesAsync(cancellationToken);
            return result > 0;
        }
    }
}
