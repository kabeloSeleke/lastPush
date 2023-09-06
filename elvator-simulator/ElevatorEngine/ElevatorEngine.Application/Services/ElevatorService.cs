using AutoMapper;
using ElevatorEngine.Application.DTOs;
using ElevatorEngine.Application.Events;
using ElevatorEngine.Application.Interfaces;
using ElevatorEngine.Domain.Interfaces;
using ElevatorEngine.Domain.Models;
using ElevatorEngine.Domain.Values;
using Microsoft.Extensions.Logging;

namespace ElevatorEngine.Application.Services
{
    public class ElevatorService : IElevatorService
    {
        private readonly IElevatorRepository _elevatorRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEventPublishService _eventPublisher;
        private readonly ILogger<ElevatorService> _logger;
        public ElevatorService(IElevatorRepository elevatorRepository, IMapper mapper, IUnitOfWork unitOfWork, IEventPublishService eventPublisher, ILogger<ElevatorService> logger)
        {
            _elevatorRepository = elevatorRepository ?? throw new ArgumentNullException(nameof(elevatorRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _eventPublisher = eventPublisher ?? throw new ArgumentNullException(nameof(eventPublisher));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public IEnumerable<ElevatorDTO> GetAllElevators()
        {
            try
            {
                var elevators = _elevatorRepository.GetAllElevators();
                return _mapper.Map<IEnumerable<ElevatorDTO>>(elevators);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving all elevators.");
                throw;
                 
            }
        }

        public ElevatorDTO GetElevatorStatus(int elevatorId)
        {
            try
            {
                Elevator elevator = _elevatorRepository.GetElevatorById(elevatorId);
                return _mapper.Map<ElevatorDTO>(elevator);
            }
            catch (Exception ex)
            {
                 _logger.LogError(ex, $"Error retrieving status for elevator with ID: {elevatorId}.");
                throw;
            }
        }

        public void UpdateElevator(ElevatorDTO elevatorDTO)
        {
            if (elevatorDTO == null) throw new ArgumentNullException(nameof(elevatorDTO));

            try
            {
                Elevator existingElevator = _elevatorRepository.GetElevatorById(elevatorDTO.Id);
                if (existingElevator != null)
                {
                    _mapper.Map(elevatorDTO, existingElevator);
                    _elevatorRepository.UpdateElevator(existingElevator);
                    _unitOfWork.Commit();
                    var elvatorEvent = new ElevatorEvent
                    {
                        ElevatorId = elevatorDTO.Id,
                        Idempotency = Guid.NewGuid(),
                        Message = "Elevator Updated",
                        Timestamp = DateTime.Now,
                        Elevator  = elevatorDTO
                    };
                    _eventPublisher.publishEvent(typeof(ElevatorEvent).Name, elvatorEvent);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error updating elevator with ID: {elevatorDTO.Id}.");
                throw;
            }
        }

        public ElevatorDTO CreateElevator(ElevatorDTO elevatorDTO)
        {
            if (elevatorDTO == null) throw new ArgumentNullException(nameof(elevatorDTO));

            try
            {
                var elevator = _mapper.Map<Elevator>(elevatorDTO);
                elevator.Status = ElevatorStatus.Idle;
                elevator.CurrentFloor = 1;
                elevator.OccupantsCount = 0;
                elevator.Direction = ElevatorDirection.None;

                _elevatorRepository.AddElevator(elevator);
                _unitOfWork.Commit();

                var elvatorEvent = new ElevatorEvent
                {
                    ElevatorId = elevatorDTO.Id,
                    Idempotency = Guid.NewGuid(),
                    Message = "Elevator Updated",
                    Timestamp = DateTime.Now,
                    Elevator = elevatorDTO
                };
                _eventPublisher.publishEvent(typeof(ElevatorEvent).Name, elvatorEvent);

                return _mapper.Map<ElevatorDTO>(elevator);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating a new elevator.");
                throw;
            }
        }

        public IEnumerable<ElevatorDTO> GetAllElevatorStatuses()
        {
            return GetAllElevators(); 
        }

       

        public ElevatorDTO GetNearestAvailableElevator(int floorId, ElevatorDirection direction)
        {
            try
            {
                var nearestElevators = _elevatorRepository.GetNearestElevator(floorId);
                var availableElevator = nearestElevators.FirstOrDefault(e => e.Status == ElevatorStatus.Idle || (e.Status == ElevatorStatus.Moving && e.RequestedDirection == direction));

                if (availableElevator == null)
                {
                    _logger.LogWarning($"No available elevators near floor: {floorId}.");
                    return null;
                }

                return _mapper.Map<ElevatorDTO>(availableElevator);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error getting nearest available elevator for floor ID: {floorId}.");
                throw;
            }
        }
    
        public void SendNearestElevatorToFloor(int floorNumber, ElevatorDirection direction)
        {
            try
            {
                var nearestElevator = _elevatorRepository.GetNearestElevator(floorNumber).FirstOrDefault();

                if (nearestElevator != null)
                {
                    AssignElevatorToFloor(nearestElevator.Id, floorNumber);
                }
                else
                {
                    _logger.LogWarning($"No elevators found near floor: {floorNumber}.");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error sending nearest elevator to floor: {floorNumber}.");
                throw;
            }
        }
        public void AssignElevatorToFloor(int elevatorId, int floorId)
        {
            try
            {
                var elevator = _elevatorRepository.GetElevatorById(elevatorId);
                if (elevator == null)
                {
                    _logger.LogWarning($"Elevator with ID: {elevatorId} not found.");
                    return;
                }

                if (elevator.CurrentFloor != floorId)  // Check to avoid unnecessary status update
                {
                    elevator.Status = ElevatorStatus.Moving;
                    elevator.RequestedDirection = elevator.CurrentFloor < floorId ? ElevatorDirection.Up : ElevatorDirection.Down;
                    // Assuming that TargetFloor property has been added to Elevator class
                    elevator.TargetFloor = floorId;

                    _elevatorRepository.UpdateElevator(elevator);
                    _logger.LogInformation($"Elevator with ID: {elevatorId} has been assigned to move to floor: {floorId}.");
                }
                else
                {
                    _logger.LogInformation($"Elevator with ID: {elevatorId} is already on the floor: {floorId}.");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error assigning elevator with ID: {elevatorId} to floor: {floorId}.");
                throw;
            }
        }


    }
}
