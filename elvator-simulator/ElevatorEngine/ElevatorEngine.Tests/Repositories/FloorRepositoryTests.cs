using ElevatorEngine.Domain.Models;
using ElevatorEngine.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
namespace ElevatorEngine.Tests.Repositories { 
public class FloorRepositoryTests
{
    private readonly ApplicationDbContext _context;
    private readonly FloorRepository _floorRepository;

    public FloorRepositoryTests()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        _context = new ApplicationDbContext(options);
        _floorRepository = new FloorRepository(_context);
    }
    [Fact]
    public void AddFloor_AddsFloorSuccessfully()
    {
        var floor = new Floor { Id = 1, FloorNumber = 1, WaitingOccupants = 5, TotalPeopleGoingUp = 3, TotalPeopleGoingDown = 2 };
        _floorRepository.AddFloor(floor);

        var result = _floorRepository.GetFloorById(1);
        Assert.NotNull(result);
        Assert.Equal(1, result.Id);
    }
    [Fact]
    public void GetFloorById_ReturnsCorrectFloor()
    {
        var floor = new Floor { Id = 2, FloorNumber = 2, WaitingOccupants = 3, TotalPeopleGoingUp = 2, TotalPeopleGoingDown = 1 };
        _context.Floors.Add(floor);
        _context.SaveChanges();
        var result = _floorRepository.GetFloorById(2);
        Assert.NotNull(result);
        Assert.Equal(2, result.Id);
    }
    [Fact]
    public void RemoveFloor_RemovesFloorSuccessfully()
    {
        var floor = new Floor { Id = 3, FloorNumber = 3, WaitingOccupants = 4, TotalPeopleGoingUp = 2, TotalPeopleGoingDown = 2 };
        _context.Floors.Add(floor);
        _context.SaveChanges();

        _floorRepository.RemoveFloor(3);
        var result = _floorRepository.GetFloorById(3);
        Assert.Null(result);
    }
    [Fact]
    public void UpdateFloor_UpdatesFloorSuccessfully()
    {
        var floor = new Floor { Id = 4, FloorNumber = 4, WaitingOccupants = 5, TotalPeopleGoingUp = 3, TotalPeopleGoingDown = 2 };
        _context.Floors.Add(floor);
        _context.SaveChanges();
        floor.WaitingOccupants = 10;
        _floorRepository.UpdateFloor(floor);
        var updatedFloor = _floorRepository.GetFloorById(4);
        Assert.Equal(10, updatedFloor.WaitingOccupants);
    }
    [Fact]
    public void GetAllFloors_ReturnsAllFloors()
    {
        var floor1 = new Floor { Id = 5, FloorNumber = 5, WaitingOccupants = 6, TotalPeopleGoingUp = 4, TotalPeopleGoingDown = 2 };
        var floor2 = new Floor { Id = 6, FloorNumber = 6, WaitingOccupants = 7, TotalPeopleGoingUp = 5, TotalPeopleGoingDown = 2 };

        _context.Floors.AddRange(floor1, floor2);
        _context.SaveChanges();

        var result = _floorRepository.GetAllFloors();

        Assert.NotNull(result);
        Assert.Equal(2, result.Count());
    }

}
}