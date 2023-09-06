using ElevatorEngine.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevatorEngine.Application.Interfaces
{
    public interface IMaintenanceService
    {
        MaintenanceRecordDTO GetMaintenanceRecord(int recordId);
        void AddMaintenanceRecord(MaintenanceRecordDTO maintenanceDTO);
    }
}