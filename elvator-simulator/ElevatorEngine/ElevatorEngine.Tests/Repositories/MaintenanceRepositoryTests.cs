using ElevatorEngine.Domain.Models;
using ElevatorEngine.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
namespace ElevatorEngine.Tests.Repositories
{

    public class MaintenanceRepositoryTests
    {
        private readonly ApplicationDbContext _context;
        private readonly MaintenanceRepository _maintenanceRepository;

        public MaintenanceRepositoryTests()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _context = new ApplicationDbContext(options);
            _maintenanceRepository = new MaintenanceRepository(_context);
        }
        [Fact]
        public void AddMaintenanceRecord_AddsRecordSuccessfully()
        {
            var record = new MaintenanceRecord { Id = 1, ElevatorId = 1, MaintenanceDate = DateTime.Now, MaintenanceSummary = "Routine check." };
            _maintenanceRepository.AddMaintenanceRecord(record);

            var result = _maintenanceRepository.GetMaintenanceRecordById(1);

            Assert.NotNull(result);
            Assert.Equal(1, result.Id);
        }
        [Fact]
        public void GetMaintenanceRecordById_ReturnsCorrectRecord()
        {
            var record = new MaintenanceRecord { Id = 1, ElevatorId = 1, MaintenanceDate = DateTime.Now, MaintenanceSummary = "Routine check" };
            _context.MaintenanceRecords.Add(record);
            _context.SaveChanges();

            var result = _maintenanceRepository.GetMaintenanceRecordById(1);

            Assert.NotNull(result);
            Assert.Equal(1, result.Id);
        }

        [Fact]
        public void RemoveMaintenanceRecord_RemovesRecordSuccessfully()
        {
            var record = new MaintenanceRecord { Id = 2, ElevatorId = 2, MaintenanceDate = DateTime.Now, MaintenanceSummary = "Oil replacement" };
            _context.MaintenanceRecords.Add(record);
            _context.SaveChanges();

            _maintenanceRepository.RemoveMaintenanceRecord(2);
            var result = _maintenanceRepository.GetMaintenanceRecordById(2);

            Assert.Null(result);
        }
        [Fact]
        public void UpdateMaintenanceRecord_UpdatesRecordSuccessfully()
        {
            var record = new MaintenanceRecord { Id = 3, ElevatorId = 3, MaintenanceDate = DateTime.Now, MaintenanceSummary = "Gear check" };
            _context.MaintenanceRecords.Add(record);
            _context.SaveChanges();

            record.MaintenanceSummary = "Gear replacement";
            _maintenanceRepository.UpdateMaintenanceRecord(record);

            var updatedRecord = _maintenanceRepository.GetMaintenanceRecordById(3);
            Assert.Equal("Gear replacement", updatedRecord.MaintenanceSummary);
        }

        [Fact]
        public void GetAllMaintenanceRecords_ReturnsAllRecords()
        {
            var record1 = new MaintenanceRecord { Id = 4, ElevatorId = 4, MaintenanceDate = DateTime.Now.AddDays(-1), MaintenanceSummary = "Cable check" };
            var record2 = new MaintenanceRecord { Id = 5, ElevatorId = 5, MaintenanceDate = DateTime.Now, MaintenanceSummary = "Door lubrication" };

            _context.MaintenanceRecords.AddRange(record1, record2);
            _context.SaveChanges();

            var result = _maintenanceRepository.GetAllMaintenanceRecords();

            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
        }
        [Fact]
        public void GetMaintenanceRecordsByElevatorId_ReturnsCorrectRecords()
        {
            var record1 = new MaintenanceRecord { Id = 6, ElevatorId = 6, MaintenanceDate = DateTime.Now.AddDays(-2), MaintenanceSummary = "Routine check" };
            var record2 = new MaintenanceRecord { Id = 7, ElevatorId = 6, MaintenanceDate = DateTime.Now, MaintenanceSummary = "Emergency stop test" };

            _context.MaintenanceRecords.AddRange(record1, record2);
            _context.SaveChanges();

            var result = _maintenanceRepository.GetMaintenanceRecordsByElevatorId(6);

            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
        }


    }
}