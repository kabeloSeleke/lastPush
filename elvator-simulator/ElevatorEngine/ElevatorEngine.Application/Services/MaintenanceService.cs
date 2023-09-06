using ElevatorEngine.Application.DTOs;
using ElevatorEngine.Application.Interfaces;
using ElevatorEngine.Domain.Interfaces;
using ElevatorEngine.Domain.Models;
using Microsoft.Extensions.Logging;
using System;

namespace ElevatorEngine.Application.Services
{
    public class MaintenanceService : IMaintenanceService
    {
        private readonly IMaintenanceRepository _maintenanceRepository;
        private readonly ILogger<MaintenanceService> _logger;
        public MaintenanceService(IMaintenanceRepository maintenanceRepository ,ILogger<MaintenanceService> logger )
        {
            _maintenanceRepository = maintenanceRepository ?? throw new ArgumentNullException(nameof(maintenanceRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public MaintenanceRecordDTO GetMaintenanceRecord(int recordId)
        {
            try
            {
                MaintenanceRecord record = _maintenanceRepository.GetMaintenanceRecordById(recordId);

                if (record == null)
                    throw new InvalidOperationException($"No maintenance record found with ID: {recordId}");

                return new MaintenanceRecordDTO
                {
                    Id = record.Id,
                    ElevatorId = record.ElevatorId,
                    MaintenanceDate = record.MaintenanceDate,
                    MaintenanceSummary = record.MaintenanceSummary
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error retrieving maintenance record with ID: {recordId}.");
                throw;
            }
              
        }

        public void AddMaintenanceRecord(MaintenanceRecordDTO maintenanceDTO)
        {
            if (maintenanceDTO == null)
                throw new ArgumentNullException(nameof(maintenanceDTO));

            try
            {
                MaintenanceRecord record = new MaintenanceRecord
                {
                    Id = maintenanceDTO.Id,
                    ElevatorId = maintenanceDTO.ElevatorId,
                    MaintenanceDate = maintenanceDTO.MaintenanceDate,
                    MaintenanceSummary = maintenanceDTO.MaintenanceSummary
                };

                _maintenanceRepository.AddMaintenanceRecord(record);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,"Error adding a new maintenance record.");
                throw;
            }
        }
    }
}
