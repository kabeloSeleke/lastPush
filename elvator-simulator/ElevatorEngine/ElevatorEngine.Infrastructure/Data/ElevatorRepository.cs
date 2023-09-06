using ElevatorEngine.Domain.Interfaces;
using ElevatorEngine.Domain.Models;
using ElevatorEngine.Domain.Values;
using ElevatorEngine.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;

public class ElevatorRepository : IElevatorRepository
{
    private readonly ApplicationDbContext _context;

    public ElevatorRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public void AddElevator(Elevator elevator)
    {
        _context.Elevators.Add(elevator);
        _context.SaveChanges();
    }

    public Elevator GetElevatorById(int id)
    {
        return _context.Elevators.FirstOrDefault(e => e.Id == id);
    }

    public void RemoveElevator(int id)
    {
        var elevator = _context.Elevators.FirstOrDefault(e => e.Id == id);
        if (elevator != null)
        {
            _context.Elevators.Remove(elevator);
            _context.SaveChanges();
        }
    }

    public void UpdateElevator(Elevator elevator)
    {
        _context.Elevators.Update(elevator);
        _context.SaveChanges();
    }

    public IEnumerable<Elevator> GetAllElevators()
    {
        return _context.Elevators.ToList();
    }

    public IEnumerable<Elevator> GetElevatorsByStatus(ElevatorStatus status)
    {
        return _context.Elevators.Where(e => e.Status == status).ToList();
    }

    public IEnumerable<Elevator> GetNearestElevator(int floorNumber)
    {
        return _context.Elevators.OrderBy(e => Math.Abs(e.CurrentFloor - floorNumber));
    }
}
