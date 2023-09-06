using Microsoft.AspNetCore.SignalR;

namespace ElevatorEngine.Infrastructure.EventHub
{
    public class ElevatorEventHub : Hub
    {
        public async Task BroadcastElevatorEvent(string eventName, object eventPayload)
        {
            await Clients.All.SendAsync(eventName, eventPayload);
        }
    }
}