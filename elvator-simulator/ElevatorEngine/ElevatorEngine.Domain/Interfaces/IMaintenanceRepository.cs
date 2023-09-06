using ElevatorEngine.Domain.Models;

namespace ElevatorEngine.Domain.Interfaces
{
    public interface IMaintenanceRepository
    {
        IEnumerable<MaintenanceRecord> GetAllMaintenanceRecords();
        MaintenanceRecord GetMaintenanceRecordById(int id);
        void AddMaintenanceRecord(MaintenanceRecord record);
        void UpdateMaintenanceRecord(MaintenanceRecord record);
        void RemoveMaintenanceRecord(int id);
        IEnumerable<MaintenanceRecord> GetMaintenanceRecordsByElevatorId(int elevatorId);
    }
}
