using ElevatorEngine.Domain.Interfaces;
using ElevatorEngine.Domain.Models;
using ElevatorEngine.Infrastructure.Data;

public class MaintenanceRepository : IMaintenanceRepository
{
    private readonly ApplicationDbContext _context;

    public MaintenanceRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public void AddMaintenanceRecord(MaintenanceRecord record)
    {
        _context.MaintenanceRecords.Add(record);
        _context.SaveChanges();
    }

    public IEnumerable<MaintenanceRecord> GetAllMaintenanceRecords()
    {
        return _context.MaintenanceRecords.ToList();
    }

    public MaintenanceRecord GetMaintenanceRecordById(int id)
    {
        return _context.MaintenanceRecords.FirstOrDefault(m => m.Id == id);
    }

    public void RemoveMaintenanceRecord(int id)
    {
        var record = _context.MaintenanceRecords.FirstOrDefault(m => m.Id == id);
        if (record != null)
        {
            _context.MaintenanceRecords.Remove(record);
            _context.SaveChanges();
        }
    }

    public void UpdateMaintenanceRecord(MaintenanceRecord record)
    {
        _context.MaintenanceRecords.Update(record);
        _context.SaveChanges();
    }

    public IEnumerable<MaintenanceRecord> GetMaintenanceRecordsByElevatorId(int elevatorId)
    {
        return _context.MaintenanceRecords.Where(m => m.ElevatorId == elevatorId).ToList();
    }
}
