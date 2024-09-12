using CommunityConnect.Data;
using CommunityConnect.Hubs;
using CommunityConnect.Models;
using MediatR;
using Microsoft.AspNetCore.SignalR;
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
        private readonly IHubContext<EventNotificationHub> _hubcontext;

        public AddEventCommandHandler(CommunityDbContext context,IHubContext<EventNotificationHub> hubcontext)
        {
            _dbcontext = context;
            _hubcontext = hubcontext;
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
            await _hubcontext.Clients.All.SendAsync("ReceiveEventNotification", $"New event added: {newEvent.Name}");
            return true;
        }
    }
}
