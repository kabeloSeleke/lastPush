using System;
using ElevatorEngine.Application.DTOs;
using ElevatorEngine.Application.Interfaces;
using ElevatorEngine.Domain.Values;

namespace ElevatorEngine.ConsoleApp.Utilities
{
    public class SimulationManager
    {
        private readonly IElevatorOrchestratorService _orchestratorService;
        private readonly IElevatorService _elevatorService;

        public SimulationManager(IElevatorService elevatorService, IElevatorOrchestratorService orchestratorService)
        {
            _elevatorService = elevatorService ?? throw new ArgumentNullException(nameof(elevatorService));
            _orchestratorService = orchestratorService ?? throw new ArgumentNullException(nameof(orchestratorService));
        }

        public void Start()
        {
            while (true) // Or any exit condition you prefer
            {
                try
                {
                    var elevators = _elevatorService.GetAllElevators();
                    DisplayManager.DisplayElevators(elevators);

                    // Handle user inputs, request elevators, display status, etc.
                    Console.WriteLine("Enter floor number to call an elevator:");
                    int floorNumber = int.Parse(Console.ReadLine());

                    Console.WriteLine("Enter number of people waiting:");
                    int numOfPeople = int.Parse(Console.ReadLine());

                    Console.WriteLine("Enter direction (Up/Down):");
                    ElevatorDirection direction = (ElevatorDirection)Enum.Parse(typeof(ElevatorDirection), Console.ReadLine(), true);

                    _orchestratorService.RequestElevatorToFloor(floorNumber, numOfPeople, direction);
                }
                catch (FormatException)
                {
                    Console.WriteLine("Invalid input. Please try again.");
                }
                catch (ArgumentException)
                {
                    Console.WriteLine("Invalid direction. Please enter 'Up' or 'Down'.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred: {ex.Message}");
                }
            }
        }
    }
}
