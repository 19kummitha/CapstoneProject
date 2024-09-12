using CommunityConnect.Data;
using MediatR;

namespace CommunityConnect.Features.Admin.Commands.UpdateEventCommand
{
    public class UpdateEventCommand : IRequest<bool>
    {
        public int EventId { get; set; }
        public string Name { get; set; }
        public DateOnly date { get; set; }
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
                return false;
            }


            eventToUpdate.Name = request.Name;
            eventToUpdate.date = request.date;
            eventToUpdate.Description = request.Description;

            _context.Events.Update(eventToUpdate);
            var result = await _context.SaveChangesAsync(cancellationToken);
            return result > 0;
        }
    }
}
