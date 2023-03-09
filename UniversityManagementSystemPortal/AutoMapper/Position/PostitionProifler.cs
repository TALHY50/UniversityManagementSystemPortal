using AutoMapper;
using UniversityManagementsystem.Models;
using UniversityManagementSystemPortal.ModelDto.Employee;
using UniversityManagementSystemPortal.ModelDto.Position;
using UniversityManagementSystemPortal.ModelDto.Program;

namespace UniversityManagementSystemPortal
{
    public class PostitionProifler : Profile
    {
        public PostitionProifler()
        {
            CreateMap<Employee, EmployeeDto>();
            CreateMap<PositionAddorUpdateDto, Position>();
        }
    }
}
