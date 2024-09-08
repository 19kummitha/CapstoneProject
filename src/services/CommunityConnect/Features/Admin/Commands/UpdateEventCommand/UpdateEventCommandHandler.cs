using CommunityConnect.Data;
using MediatR;

namespace CommunityConnect.Features.Admin.Commands.UpdateEventCommand
{
    public class UpdateEventCommand : IRequest<bool>
    {
        public int EventId { get; set; }
        public string Title { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public string Description { get; set; }
    }
    public class UpdateEventCommandHandler : IRequestHandler<UpdateEventCommand, bool>
    {
        private readonly CommunityDbContext _context;

        public UpdateEventCommandHandler(CommunityDbContext context)
        {
            _context = context;
        }
        public async Task<bool> Handle(UpdateEventCommand request, CancellationToken cancellationToken)
        {
            var eventToUpdate = await _context.Events.FindAsync(request.EventId);

            if (eventToUpdate == null)
            {
                // Event not found
                return false;
            }

            // Update event details
            eventToUpdate.Title = request.Title;
            eventToUpdate.Start = request.Start;
            eventToUpdate.End = request.End;
            eventToUpdate.Description = request.Description;

            _context.Events.Update(eventToUpdate);
            var result = await _context.SaveChangesAsync(cancellationToken);
            return result > 0;
        }
    }
}
