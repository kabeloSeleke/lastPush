using ElevatorEngine.Application.DTOs;
using ElevatorEngine.Application.Interfaces;
using ElevatorEngine.Domain.Models;
using ElevatorEngine.Domain.Values;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;

namespace ElevatorEngine.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ElevatorController : ControllerBase
    {
        private readonly IElevatorService _elevatorService;

        public ElevatorController(IElevatorService elevatorService)
        {
            _elevatorService = elevatorService;
        }

        [HttpGet("GetElevatorStatus{id}")]
        public IActionResult GetElevatorStatus(int id)
        {
            try
            {
                var elevator = _elevatorService.GetElevatorStatus(id);
                return Ok(elevator);
            }
            catch (Exception e)   //middlewear will handle this exception and log it 
            {
                throw  ;
            }
            
        }

        [HttpPost("UpdateElevator")]
        public IActionResult UpdateElevator(ElevatorDTO elevator)
        {
            try
            {
                _elevatorService.UpdateElevator(elevator);
                return Ok();
            }
            catch (Exception e)
            {
                throw  ;
            }

         
        }
        [HttpPost("CreateElevator")]
        public IActionResult CreateElevator()
        {
            try
            {
                var random = new Random();
                var elevatorDTO = new ElevatorDTO
                {
                    CurrentFloor = random.Next(1, 10),
                    Status = ElevatorStatus.Idle,
                    Direction = ElevatorDirection.None,
                    OccupantsCount = random.Next(0, 10)
                };

                var savedElevator = _elevatorService.CreateElevator(elevatorDTO);

                return Ok(savedElevator);
            }
            catch (Exception e)
            {
                throw;
            }

            
        }


        [HttpGet("GetAllElevators")]
        public IActionResult GetAllElevators()
        {
            try
            {
                var elevators = _elevatorService.GetAllElevators();
                return Ok(elevators);
            }
            catch (Exception e)
            {
                throw;
            }
           
        }
        [HttpGet("GetAllElevatorStatuses")]
        public IActionResult GetAllElevatorStatuses()
        {
            try
            {
                var elevators = _elevatorService.GetAllElevatorStatuses();
                return Ok(elevators);
            }
            catch (Exception e)
            {
                throw;
            }
          
        }
    }
}

