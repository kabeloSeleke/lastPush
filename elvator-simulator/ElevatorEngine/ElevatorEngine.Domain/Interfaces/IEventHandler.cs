

namespace ElevatorEngine.Domain.Interfaces
{
    public interface IEventHandler<TEvent>
    {
        void Handle(TEvent domainEvent);
    }
}
