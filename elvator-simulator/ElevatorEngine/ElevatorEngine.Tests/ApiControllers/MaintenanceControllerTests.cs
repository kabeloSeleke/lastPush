using ElevatorEngine.Api.Controllers;
using ElevatorEngine.Application.DTOs;
using ElevatorEngine.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace ElevatorEngine.Tests.ApiControllers
{
    public class MaintenanceControllerTests
    {
        private readonly MaintenanceController _controller;
        private readonly Mock<IMaintenanceService> _mockService;

        public MaintenanceControllerTests()
        {
            _mockService = new Mock<IMaintenanceService>();
            _controller = new MaintenanceController(_mockService.Object);
        }

        [Fact]
        public void GetMaintenanceRecord_ReturnsOkResult_WithRecordData()
        {
            var id = 1;
            var record = new MaintenanceRecordDTO();
            _mockService.Setup(s => s.GetMaintenanceRecord(id)).Returns(record);

            var result = _controller.GetMaintenanceRecord(id);

            Assert.IsType<OkObjectResult>(result);
            var okResult = result as OkObjectResult;
            Assert.Equal(record, okResult.Value);
        }

        [Fact]
        public void AddMaintenanceRecord_ReturnsOkResult()
        {
            var record = new MaintenanceRecordDTO();
            _mockService.Setup(s => s.AddMaintenanceRecord(record)).Verifiable();

            var result = _controller.AddMaintenanceRecord(record);

            Assert.IsType<OkResult>(result);
            _mockService.Verify();
        }
 
    }
}
