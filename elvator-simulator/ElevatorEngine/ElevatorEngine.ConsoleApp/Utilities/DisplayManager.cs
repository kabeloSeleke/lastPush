 using ElevatorEngine.Application.DTOs;
using ElevatorEngine.Application.Interfaces;
using System;
using System.Collections.Generic;

namespace ElevatorEngine.ConsoleApp.Utilities
{
    public class DisplayManager
    {
        public static void DisplayElevators(IEnumerable<ElevatorDTO> elevators)
        {
            Console.WriteLine("Elevator Status:\n");
            foreach (var elevator in elevators)
            {
                Console.WriteLine($"ID: {elevator.Id}");
                Console.WriteLine($"Current Floor: {elevator.CurrentFloor}");
                Console.WriteLine($"Direction: {elevator.Direction}");
                Console.WriteLine($"Occupants: {elevator.OccupantsCount}");
                Console.WriteLine("------------------------------------");
            }
        }

   
    }
}
