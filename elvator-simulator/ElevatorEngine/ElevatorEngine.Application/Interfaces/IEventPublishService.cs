using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ElevatorEngine.Application.Interfaces
{
    public  interface IEventPublishService
    {
        void publishEvent(string method, object triggeredEvent);
    }
}
