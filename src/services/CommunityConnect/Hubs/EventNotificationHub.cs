using Microsoft.AspNetCore.SignalR;

namespace CommunityConnect.Hubs
{
    public class EventNotificationHub:Hub
    {
        public async Task SendEventNotification(string Message)
        {
            await Clients.All.SendAsync("EventNotification",Message);
        }
    }
}
