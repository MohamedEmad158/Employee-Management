using AutoMapper;
using EmployeeManagementApi.Domain.Models;
using EmployeeManagementApi.DTOs;

namespace EmployeeManagementApi.DTOs
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Employee, EmployeeDto>().ReverseMap();
            CreateMap<Employee, EmployeeDataDto>().ReverseMap();
        }
    }
}