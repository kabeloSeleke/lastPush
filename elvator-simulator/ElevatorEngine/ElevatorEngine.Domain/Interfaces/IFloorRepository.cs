using ElevatorEngine.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevatorEngine.Domain.Interfaces
{
    public interface IFloorRepository
    {
        IEnumerable<Floor> GetAllFloors();
        Floor GetFloorById(int id);
        void AddFloor(Floor floor);
        void UpdateFloor(Floor floor);
        void RemoveFloor(int id);

    }
}
