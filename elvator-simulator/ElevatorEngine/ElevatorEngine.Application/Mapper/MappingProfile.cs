using AutoMapper;
using ElevatorEngine.Application.DTOs;
using ElevatorEngine.Domain.Models;

namespace ElevatorEngine.Application.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Elevator, ElevatorDTO>().ReverseMap();
            CreateMap<Floor, FloorDTO>().ReverseMap();
            CreateMap<MaintenanceRecord, MaintenanceRecordDTO>().ReverseMap();
         
        }
    }
}