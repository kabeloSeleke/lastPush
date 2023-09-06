using ElevatorEngine.Domain.Models;
using ElevatorEngine.Domain.Values;

namespace ElevatorEngine.Domain.Interfaces
{
    public interface IElevatorRepository
    {
        IEnumerable<Elevator> GetAllElevators();
        Elevator GetElevatorById(int id);
        void AddElevator(Elevator elevator);
        void UpdateElevator(Elevator elevator);
        void RemoveElevator(int id);
        IEnumerable<Elevator> GetElevatorsByStatus(ElevatorStatus status);
        IEnumerable<Elevator> GetNearestElevator(int floorNumber);
    }
}
