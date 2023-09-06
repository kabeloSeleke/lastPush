using ElevatorEngine.Application.DTOs;
using ElevatorEngine.Domain.Values;

namespace ElevatorEngine.Application.Interfaces
{
    public interface IElevatorService
    {
        ElevatorDTO GetElevatorStatus(int elevatorId);
        void UpdateElevator(ElevatorDTO elevatorDTO);
        ElevatorDTO CreateElevator(ElevatorDTO elevatorDTO);
        IEnumerable<ElevatorDTO> GetAllElevators();

        IEnumerable<ElevatorDTO> GetAllElevatorStatuses();
        ElevatorDTO GetNearestAvailableElevator(int floorId, ElevatorDirection direction);
        void SendNearestElevatorToFloor(int floorNumber, ElevatorDirection direction);

 
    }
}

