

namespace ElevatorEngine.Application.DTOs
{
    public class MaintenanceRecordDTO
    {
        public int Id { get; set; }
        public int ElevatorId { get; set; }
        public DateTime MaintenanceDate { get; set; }
        public string? MaintenanceSummary { get; set; }
    }

}