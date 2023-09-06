using ElevatorEngine.Application.DTOs;
using ElevatorEngine.Domain.Models;
using System;

namespace ElevatorEngine.Application.Events
{
    public class ElevatorEvent
    {
        public int ElevatorId { get; set; }
        public Guid Idempotency { get; set; }
        public string Message { get; set; }
        public DateTime Timestamp { get; set; }
        public  ElevatorDTO Elevator { get; set; }
    }
}
