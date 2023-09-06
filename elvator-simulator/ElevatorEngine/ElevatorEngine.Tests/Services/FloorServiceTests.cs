using ElevatorEngine.Application.DTOs;
using ElevatorEngine.Application.Services;
using ElevatorEngine.Domain.Interfaces;
using ElevatorEngine.Domain.Models;
using Moq;
using AutoMapper;
using ElevatorEngine.Application.Mapper;
using Microsoft.Extensions.Logging;

namespace ElevatorEngine.Tests.Services
{
    public class FloorServiceTests
    {
        private readonly FloorService _floorService;
        private readonly Mock<IFloorRepository> _floorRepositoryMock = new Mock<IFloorRepository>();
        private readonly Mock<ILogger<FloorService>> _loggerMock = new Mock<ILogger<FloorService>>();
        private readonly Mock<IUnitOfWork> _unitOfWorkMock = new Mock<IUnitOfWork>();
        private readonly IMapper _mapper;

        public FloorServiceTests()
        {
            var config = new MapperConfiguration(cfg => cfg.AddProfile(new MappingProfile()));
            _mapper = config.CreateMapper();

            _floorService = new FloorService(_floorRepositoryMock.Object, _mapper, _unitOfWorkMock.Object ,_loggerMock.Object);
        }

        [Fact]
        public void GetAllFloorStatuses_ReturnsAllFloors()
        { 
            var floors = new List<Floor>
            {
                new Floor { Id = 1 },
                new Floor { Id = 2 }
            };

            _floorRepositoryMock.Setup(repo => repo.GetAllFloors()).Returns(floors);
            var result = _floorService.GetAllFloorStatuses();
            Assert.Equal(2, result.Count());
        }

        [Fact]
        public void GetFloorStatus_ValidId_ReturnsCorrectFloor()
        {
             
            var floor = new Floor { Id = 1, FloorNumber = 3 };
            _floorRepositoryMock.Setup(repo => repo.GetFloorById(1)).Returns(floor);
            var result = _floorService.GetFloorStatus(1);
            Assert.Equal(1, result.Id);
            Assert.Equal(3, result.FloorNumber);
        }

        [Fact]
        public void UpdateFloor_ValidDto_UpdatesFloor()
        {
             
            var floor = new Floor { Id = 1 };
            var floorDTO = new FloorDTO { Id = 1, FloorNumber = 5 };
            _floorRepositoryMock.Setup(repo => repo.GetFloorById(1)).Returns(floor);
            _floorService.UpdateFloor(floorDTO);         
            _floorRepositoryMock.Verify(repo => repo.UpdateFloor(It.IsAny<Floor>()), Times.Once);
        }

        [Fact]
        public void CreateFloor_ValidDto_CreatesFloor()
        {
             
            var floorDTO = new FloorDTO { Id = 1, FloorNumber = 2 };
            var result = _floorService.CreateFloor(floorDTO);
            Assert.Equal(2, result.FloorNumber);
            _floorRepositoryMock.Verify(repo => repo.AddFloor(It.IsAny<Floor>()), Times.Once);
            _unitOfWorkMock.Verify(uow => uow.Commit(), Times.Once);
        }
    }
}
