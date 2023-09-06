using ElevatorEngine.Domain.Interfaces;
using ElevatorEngine.Domain.Models;

namespace ElevatorEngine.Infrastructure.Data
{
    public class FloorRepository : IFloorRepository
    {
        private readonly ApplicationDbContext _context;

        public FloorRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void AddFloor(Floor floor)
        {
            _context.Floors.Add(floor);
            _context.SaveChanges();
        }

        public IEnumerable<Floor> GetAllFloors()
        {
            return _context.Floors.ToList();
        }

        public Floor GetFloorById(int id)
        {
            return _context.Floors.FirstOrDefault(f => f.Id == id);
        }

        public void RemoveFloor(int id)
        {
            var floor = _context.Floors.Find(id);
            if (floor != null)
            {
                _context.Floors.Remove(floor);
                _context.SaveChanges();
            }
        }

        public void UpdateFloor(Floor floor)
        {
            var existingFloor = _context.Floors.Find(floor.Id);
            if (existingFloor != null)
            {
                _context.Entry(existingFloor).CurrentValues.SetValues(floor);
                _context.SaveChanges();
            }
        }
    }
}
