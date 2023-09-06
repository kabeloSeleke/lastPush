using ElevatorEngine.Application.DTOs;
using ElevatorEngine.Application.Interfaces;
using ElevatorEngine.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace ElevatorEngine.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MaintenanceController : ControllerBase
    {
        private readonly IMaintenanceService _maintenanceService;

        public MaintenanceController(IMaintenanceService maintenanceService)
        {
            _maintenanceService = maintenanceService;
        }

        [HttpGet("GetMaintenanceRecord{id}")]
        public IActionResult GetMaintenanceRecord(int id)
        {
            try
            {
                var record = _maintenanceService.GetMaintenanceRecord(id);
                return Ok(record);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        
    

        [HttpPost("AddMaintenanceRecord")]
        public IActionResult AddMaintenanceRecord(MaintenanceRecordDTO maintenanceRecord)
        {
            try
            {
                _maintenanceService.AddMaintenanceRecord(maintenanceRecord);
                return Ok();
            }
            catch (Exception ex)
            {
                throw;
            }
           
        }
    }
}