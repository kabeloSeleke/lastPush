using ElevatorEngine.Api.Controllers;
using ElevatorEngine.Application.DTOs;
using ElevatorEngine.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace ElevatorEngine.Tests.ApiControllers
{
    public class FloorControllerTests
    {
        private readonly FloorController _controller;
        private readonly Mock<IFloorService> _mockService;

        public FloorControllerTests()
        {
            _mockService = new Mock<IFloorService>();
            _controller = new FloorController(_mockService.Object);
        }

        [Fact]
        public void GetFloorStatus_ReturnsOkResult_WithFloorData()
        {
            var id = 1;
            var floor = new FloorDTO();
            _mockService.Setup(s => s.GetFloorStatus(id)).Returns(floor);

            var result = _controller.GetFloorStatus(id);

            Assert.IsType<OkObjectResult>(result);
            var okResult = result as OkObjectResult;
            Assert.Equal(floor, okResult.Value);
        }

        [Fact]
        public void UpdateFloor_ReturnsOkResult()
        {
            var floor = new FloorDTO();
            _mockService.Setup(s => s.UpdateFloor(floor)).Verifiable();

            var result = _controller.UpdateFloor(floor);

            Assert.IsType<OkResult>(result);
            _mockService.Verify();
        }

    }
}

