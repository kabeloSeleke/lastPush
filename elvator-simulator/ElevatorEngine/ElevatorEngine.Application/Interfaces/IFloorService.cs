using ElevatorEngine.Application.DTOs;
using ElevatorEngine.Domain.Values;


namespace ElevatorEngine.Application.Interfaces
{
    public interface IFloorService
    {
        FloorDTO GetFloorStatus(int floorId);
        void UpdateFloor(FloorDTO floorDTO);
        FloorDTO CreateFloor(FloorDTO floorDTO);
        void RequestElevatorToFloor(int floorId, int numOfPeople, ElevatorDirection direction);
        IEnumerable<FloorDTO> GetAllFloorStatuses();
        void UpdateFloorOccupants(int floorId, int numOfPeople, ElevatorDirection direction);
    }
}