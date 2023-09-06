using ElevatorEngine.Application.DTOs;
using ElevatorEngine.Application.Services;
using ElevatorEngine.Domain.Interfaces;
using ElevatorEngine.Domain.Models;
using Moq;
using AutoMapper;
using ElevatorEngine.Domain.Values;
using ElevatorEngine.Application.Mapper;
using ElevatorEngine.Application.Interfaces;
using Microsoft.Extensions.Logging;

namespace ElevatorEngine.Tests.Services
{
    public class ElevatorServiceTests
    {
        private readonly ElevatorService _elevatorService;
        private readonly Mock<IElevatorRepository> _elevatorRepositoryMock = new Mock<IElevatorRepository>();
        private readonly Mock<ILogger<ElevatorService>> _loggerMock = new Mock<ILogger<ElevatorService>>();   
        private readonly Mock<IUnitOfWork> _unitOfWorkMock = new Mock<IUnitOfWork>();
        private readonly IMapper _mapper;

        public ElevatorServiceTests()
        {
            var config = new MapperConfiguration(cfg => cfg.AddProfile(new MappingProfile()));
            _mapper = config.CreateMapper();
            _elevatorService = new ElevatorService(_elevatorRepositoryMock.Object, _mapper, _unitOfWorkMock.Object, _loggerMock.Object);
        }

        [Fact]
        public void GetAllElevators_ReturnsAllElevators()
        {
    
            var elevators = new List<Elevator>
            {
                new Elevator { Id = 1 },
                new Elevator { Id = 2 }
            };

            _elevatorRepositoryMock.Setup(repo => repo.GetAllElevators()).Returns(elevators);
            var result = _elevatorService.GetAllElevators();
            Assert.Equal(2, result.Count());
        }

        [Fact]
        public void GetElevatorStatus_ValidId_ReturnsCorrectElevator()
        {
             
            var elevator = new Elevator { Id = 1, CurrentFloor = 3 };
            _elevatorRepositoryMock.Setup(repo => repo.GetElevatorById(1)).Returns(elevator);
            var result = _elevatorService.GetElevatorStatus(1);
            Assert.Equal(1, result.Id);
            Assert.Equal(3, result.CurrentFloor);
        }

        [Fact]
        public void UpdateElevator_ValidDto_UpdatesElevator()
        {
             
            var elevator = new Elevator { Id = 1 };
            var elevatorDTO = new ElevatorDTO { Id = 1, CurrentFloor = 5 };
            _elevatorRepositoryMock.Setup(repo => repo.GetElevatorById(1)).Returns(elevator);
            _elevatorService.UpdateElevator(elevatorDTO);
            _elevatorRepositoryMock.Verify(repo => repo.UpdateElevator(It.IsAny<Elevator>()), Times.Once);
            _unitOfWorkMock.Verify(uow => uow.Commit(), Times.Once);
        }

        [Fact]
        public void CreateElevator_ValidDto_CreatesElevator()
        {
            
            var elevatorDTO = new ElevatorDTO { Id = 1, WeightLimit = 500 };   
            var result = _elevatorService.CreateElevator(elevatorDTO);
            Assert.Equal(ElevatorStatus.Idle, result.Status);
            Assert.Equal(1, result.CurrentFloor);
            Assert.Equal(0, result.OccupantsCount);
            Assert.Equal(ElevatorDirection.None, result.Direction);
            _elevatorRepositoryMock.Verify(repo => repo.AddElevator(It.IsAny<Elevator>()), Times.Once);
            _unitOfWorkMock.Verify(uow => uow.Commit(), Times.Once);
        }

         
    }
}
