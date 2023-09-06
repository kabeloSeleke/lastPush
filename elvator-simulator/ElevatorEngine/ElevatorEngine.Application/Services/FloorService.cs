using AutoMapper;
using ElevatorEngine.Application.DTOs;
using ElevatorEngine.Application.Interfaces;
using ElevatorEngine.Domain.Interfaces;
using ElevatorEngine.Domain.Models;
using ElevatorEngine.Domain.Values;
using Microsoft.Extensions.Logging;

namespace ElevatorEngine.Application.Services
{
    public class FloorService : IFloorService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFloorRepository _floorRepository;
        private readonly ILogger<FloorService> _logger;
        public FloorService(IFloorRepository floorRepository, IMapper mapper, IUnitOfWork unitOfWork, ILogger<FloorService> logger)
        {
            _floorRepository = floorRepository ?? throw new ArgumentNullException(nameof(floorRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger)); // Ensure logger isn't null
        }

        public FloorDTO GetFloorStatus(int floorId)
        {
            try
            {
                Floor floor = _floorRepository.GetFloorById(floorId);
                if (floor == null) throw new InvalidOperationException($"No floor found with ID: {floorId}");

                return new FloorDTO
                {
                    Id = floor.Id,
                    FloorNumber = floor.FloorNumber,
                    WaitingOccupants = floor.WaitingOccupants
                };
            }
            catch (Exception ex)
            {
               _logger.LogError(ex, $"Error retrieving status for floor with ID: {floorId}.");
                throw;
            }
        }

        public void UpdateFloor(FloorDTO floorDTO)
        {
            if (floorDTO == null) throw new ArgumentNullException(nameof(floorDTO));

            try
            {
                Floor existingFloor = _floorRepository.GetFloorById(floorDTO.Id);
                if (existingFloor != null)
                {
                    existingFloor.FloorNumber = floorDTO.FloorNumber;
                    existingFloor.WaitingOccupants = floorDTO.WaitingOccupants;
                    _floorRepository.UpdateFloor(existingFloor);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,$"Error updating floor with ID: {floorDTO.Id}.");
                throw;
              
            }
        }

        public FloorDTO CreateFloor(FloorDTO floorDTO)
        {
            if (floorDTO == null) throw new ArgumentNullException(nameof(floorDTO));

            try
            {
                var floor = _mapper.Map<Floor>(floorDTO);
                _floorRepository.AddFloor(floor);
                _unitOfWork.Commit();

                return _mapper.Map<FloorDTO>(floor);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating a new floor.");
                throw;
            }
        }

        public void RequestElevatorToFloor(int floorId, int numOfPeople, ElevatorDirection direction)
        {
            try
            {
                Floor floor = _floorRepository.GetFloorById(floorId);

                if (floor == null) throw new InvalidOperationException($"No floor found with ID: {floorId}");

                if (direction == ElevatorDirection.Up)
                {
                    floor.TotalPeopleGoingUp += numOfPeople;
                }
                else if (direction == ElevatorDirection.Down)
                {
                    floor.TotalPeopleGoingDown += numOfPeople;
                }

                floor.WaitingOccupants += numOfPeople;
                _floorRepository.UpdateFloor(floor);
            }
            catch (Exception ex)
            {
 
                _logger.LogError(ex, $"Error requesting elevator to floor with ID: {floorId}.");
                throw;
            }
        }

        public IEnumerable<FloorDTO> GetAllFloorStatuses()
        {
            try
            {
                var floors = _floorRepository.GetAllFloors();
                return _mapper.Map<IEnumerable<FloorDTO>>(floors);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving all floor statuses");
                throw;
               
            }
        }

        public void UpdateFloorOccupants(int floorId, int numOfPeople, ElevatorDirection direction)
        {
            throw new NotImplementedException();
        }
    }
}
