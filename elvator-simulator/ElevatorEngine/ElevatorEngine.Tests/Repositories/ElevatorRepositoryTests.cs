using ElevatorEngine.Domain.Models;
using ElevatorEngine.Domain.Values;
using ElevatorEngine.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
namespace ElevatorEngine.Tests.Repositories
{
    public class ElevatorRepositoryTests
    {
        private readonly ApplicationDbContext _context;
        private readonly ElevatorRepository _elevatorRepository;

        public ElevatorRepositoryTests()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _context = new ApplicationDbContext(options);
            _elevatorRepository = new ElevatorRepository(_context);
        }

        [Fact]
        public void AddElevator_AddsElevatorSuccessfully()
        {

            var elevator = new Elevator
            {
                Id = 1,
                CurrentFloor = 1,
                Status = ElevatorStatus.Idle,
                Direction = ElevatorDirection.None
            };

            _elevatorRepository.AddElevator(elevator);

            var result = _elevatorRepository.GetElevatorById(1);
            Assert.NotNull(result);
            Assert.Equal(1, result.Id);
        }

        [Fact]
        public void GetElevatorById_ReturnsCorrectElevator()
        {
            var elevator = new Elevator { Id = 2, CurrentFloor = 2, Status = ElevatorStatus.Idle, Direction = ElevatorDirection.None };
            _context.Elevators.Add(elevator);
            _context.SaveChanges();
            var result = _elevatorRepository.GetElevatorById(2);
            Assert.NotNull(result);
            Assert.Equal(2, result.Id);
        }
        [Fact]
        public void RemoveElevator_RemovesElevatorSuccessfully()
        {
            var elevator = new Elevator { Id = 3, CurrentFloor = 3, Status = ElevatorStatus.Idle, Direction = ElevatorDirection.None };
            _context.Elevators.Add(elevator);
            _context.SaveChanges();

            _elevatorRepository.RemoveElevator(3);
            var result = _elevatorRepository.GetElevatorById(3);

            Assert.Null(result);
        }





    }
}
