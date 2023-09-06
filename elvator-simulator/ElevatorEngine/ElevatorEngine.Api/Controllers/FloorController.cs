// FloorController.cs
using ElevatorEngine.Application.DTOs;
using ElevatorEngine.Application.Interfaces;
using ElevatorEngine.Domain.Models;
using ElevatorEngine.Domain.Values;
using Microsoft.AspNetCore.Mvc;

namespace ElevatorEngine.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FloorController : ControllerBase
    {
        private readonly IFloorService _floorService;

        public FloorController(IFloorService floorService)
        {
            _floorService = floorService;
        }

        [HttpGet("{GetFloorStatusid}")]
        public IActionResult GetFloorStatus(int id)
        {
            try
            {
                var floor = _floorService.GetFloorStatus(id);
                return Ok(floor);
            }
            catch (Exception ex)
            {
                throw;
            }
            
        }

        [HttpPost("UpdateFloor")]
        public IActionResult UpdateFloor(FloorDTO floor)
        {
            try
            {
                _floorService.UpdateFloor(floor);
                return Ok();
            }
            catch (Exception ex)
            {
                throw;
            }
           
        }
        [HttpPost("CreateFloor")]
        public IActionResult CreateFloor()
        {
            try
            {
                var random = new Random();
                var floorDTO = new FloorDTO
                {
                    FloorNumber = random.Next(1, 100),
                    WaitingOccupants = random.Next(0, 10)
                };

                var savedFloor = _floorService.CreateFloor(floorDTO);

                return Ok(savedFloor);
            }
            catch (Exception ex)
            {
                throw;
            }
           
        }
        [HttpPost("RequestElevator")]
        public IActionResult RequestElevatorToFloor(int floorId, int numOfPeople, ElevatorDirection direction)
        {
            try
            {
                _floorService.RequestElevatorToFloor(floorId, numOfPeople, direction);
                return Ok();
            }
            catch (Exception ex)
            {
                throw;
            }
           
        }

        [HttpGet("GetAllFloorStatuses")]
        public IActionResult GetAllFloorStatuses()
        {
            try
            {
                var floors = _floorService.GetAllFloorStatuses();
                return Ok(floors);
            }
            catch (Exception ex)
            {
                throw;
            }
          
        }

    }
}