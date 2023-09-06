using ElevatorEngine.Application.Interfaces;
using ElevatorEngine.Domain.Values;

namespace ElevatorEngine.Application.Services
 
    {
         
        public class ElevatorOrchestratorService :IElevatorOrchestratorService
        {
            private readonly IElevatorService _elevatorService;
            private readonly IFloorService _floorService;

            public ElevatorOrchestratorService(IElevatorService elevatorService, IFloorService floorService)
            {
                _elevatorService = elevatorService ?? throw new ArgumentNullException(nameof(elevatorService));
                _floorService = floorService ?? throw new ArgumentNullException(nameof(floorService));
            }

            public void RequestElevatorToFloor(int floorId, int numOfPeople, ElevatorDirection direction)
            {
                try
                {
             
                    _floorService.UpdateFloorOccupants(floorId, numOfPeople, direction);

                    var nearestElevator = _elevatorService.GetNearestAvailableElevator(floorId, direction);
                    _elevatorService.SendNearestElevatorToFloor(nearestElevator.Id, floorId);
                }
                catch (Exception ex)
                { 
                    throw;
                }
            }
        }
    }