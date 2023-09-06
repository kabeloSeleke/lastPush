using ElevatorEngine.Application.DTOs;
using ElevatorEngine.Application.Services;
using ElevatorEngine.Domain.Interfaces;
using ElevatorEngine.Domain.Models;
using Microsoft.Extensions.Logging;
using Moq;


namespace ElevatorEngine.Tests.Services
{
    public class MaintenanceServiceTests
    {
        private readonly MaintenanceService _maintenanceService;
        private readonly Mock<IMaintenanceRepository> _maintenanceRepositoryMock = new Mock<IMaintenanceRepository>();
        private readonly Mock<ILogger<MaintenanceService>> _loggerMock = new Mock<ILogger<MaintenanceService>>();
        public MaintenanceServiceTests()
        {
            _maintenanceService = new MaintenanceService(_maintenanceRepositoryMock.Object, _loggerMock.Object);
        }

        [Fact]
        public void GetMaintenanceRecord_ValidId_ReturnsCorrectRecord()
        {
            var record = new MaintenanceRecord { Id = 1, ElevatorId = 2 };
            _maintenanceRepositoryMock.Setup(repo => repo.GetMaintenanceRecordById(1)).Returns(record);

            var result = _maintenanceService.GetMaintenanceRecord(1);
            Assert.Equal(1, result.Id);
            Assert.Equal(2, result.ElevatorId);
        }

        [Fact]
        public void AddMaintenanceRecord_ValidDto_AddsRecord()
        {
            var maintenanceDTO = new MaintenanceRecordDTO { Id = 1, ElevatorId = 2, MaintenanceSummary = "Test" };
            _maintenanceService.AddMaintenanceRecord(maintenanceDTO);
            _maintenanceRepositoryMock.Verify(repo => repo.AddMaintenanceRecord(It.IsAny<MaintenanceRecord>()), Times.Once);
        }
    }
}
