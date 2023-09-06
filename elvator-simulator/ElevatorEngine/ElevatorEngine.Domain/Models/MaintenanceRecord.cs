

namespace ElevatorEngine.Domain.Models
{
    public class MaintenanceRecord
    {
        public int Id { get; set; }
        public int ElevatorId { get; set; }
        public DateTime MaintenanceDate { get; set; }
        public string MaintenanceSummary { get; set; }
    }
}
