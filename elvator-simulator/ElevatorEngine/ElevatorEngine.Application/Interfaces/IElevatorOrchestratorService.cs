using ElevatorEngine.Domain.Values;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevatorEngine.Application.Interfaces
{
    public interface IElevatorOrchestratorService
    {
        void RequestElevatorToFloor(int floorId, int numOfPeople, ElevatorDirection direction);
    }
}
