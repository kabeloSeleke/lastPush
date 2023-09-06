using ElevatorEngine.Api.Controllers;
using ElevatorEngine.Application.DTOs;
using ElevatorEngine.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace ElevatorEngine.Tests.ApiControllers
{
    public class ElevatorControllerTests
    {
        private readonly ElevatorController _controller;
        private readonly Mock<IElevatorService> _mockService;

        public ElevatorControllerTests()
        {
            _mockService = new Mock<IElevatorService>();
            _controller = new ElevatorController(_mockService.Object);
        }

        [Fact]
        public void GetElevatorStatus_ReturnsOkResult_WithElevatorData()
        {
            var id = 1;
            var elevator = new ElevatorDTO();
            _mockService.Setup(s => s.GetElevatorStatus(id)).Returns(elevator);

            var result = _controller.GetElevatorStatus(id);

            Assert.IsType<OkObjectResult>(result);
            var okResult = result as OkObjectResult;
            Assert.Equal(elevator, okResult.Value);
        }

        [Fact]
        public void UpdateElevator_ReturnsOkResult()
        {
            var elevator = new ElevatorDTO();
            _mockService.Setup(s => s.UpdateElevator(elevator)).Verifiable();

            var result = _controller.UpdateElevator(elevator);

            Assert.IsType<OkResult>(result);
            _mockService.Verify();
        }

        [Fact]
        public void GetAllElevators_ReturnsOkResult_WithElevatorList()
        {
            var elevators = new List<ElevatorDTO> { new ElevatorDTO(), new ElevatorDTO() };
            _mockService.Setup(s => s.GetAllElevators()).Returns(elevators);

            var result = _controller.GetAllElevators();

            Assert.IsType<OkObjectResult>(result);
            var okResult = result as OkObjectResult;
            Assert.Equal(elevators, okResult.Value);
        }


    }
}
