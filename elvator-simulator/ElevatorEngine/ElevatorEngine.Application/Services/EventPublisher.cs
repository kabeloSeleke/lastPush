using ElevatorEngine.Application.Interfaces;
using ElevatorEngine.Infrastructure.EventHub;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;

namespace ElevatorEngine.Application.Services
{
    public class EventPublisher : IEventPublishService
    {
        //private readonly IHubContext<ElevatorEventHub> _hubContext;
        //private readonly ILogger<EventPublisher> _logger;   
        //public EventPublisher(IHubContext<ElevatorEventHub> hubContext,ILogger<EventPublisher> logger)
        //{
        //    _hubContext = hubContext ?? throw new ArgumentNullException(nameof(hubContext));
        //    _logger = logger ?? throw new ArgumentNullException(nameof(logger));  
        //}

        //public void publishEvent(string eventName, object triggeredEvent)
        //{
        //    if (string.IsNullOrEmpty(eventName)) throw new ArgumentNullException(nameof(eventName));

        //    try
        //    {
        //        _hubContext.Clients.All.SendAsync(eventName, triggeredEvent).Wait();  
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex, "Error publishing events.");
        //        throw;  
        //    }
        //}
        public void publishEvent(string method, object triggeredEvent)
        {
           
        }
    }
}
