using CommunityConnect.Data;
using CommunityConnect.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CommunityConnect.Features.Admin.Commands.AddEventCommand
{
    public class CreateEventCommand : IRequest<bool>
    {
        public string Name { get; set; }
        public DateOnly date { get; set; }
        public string Description { get; set; }
    }
    public class AddEventCommandHandler:IRequestHandler<CreateEventCommand,bool>
    {
        private readonly CommunityDbContext _dbcontext;

        public AddEventCommandHandler(CommunityDbContext context)
        {
            _dbcontext = context;
        }

        public async Task<bool> Handle(CreateEventCommand request, CancellationToken cancellationToken)
        {
            var newEvent = new Event
            {
                Name= request.Name,
                date= request.date,
                Description = request.Description
            };

            _dbcontext.Events.Add(newEvent);
            await _dbcontext.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}
