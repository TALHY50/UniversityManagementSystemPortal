using AutoMapper;
using UniversityManagementsystem.Models;
using UniversityManagementSystemPortal.ModelDto.Employee;

namespace UniversityManagementSystemPortal
{
    public class EmployeeProfiler : Profile
    {
        public EmployeeProfiler() {
            CreateMap<Employee, EmployeeDto>();

            // Map from CreateEmployeeDto to Employee entity
            CreateMap<CreateEmployeeDto, Employee>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid()))
                .ForMember(dest => dest.ProfilePath, opt => opt.Ignore())
                .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => true));

            // Map from UpdateEmployeeDto to Employee entity
            CreateMap<UpdateEmployeeDto, Employee>()
                .ForMember(dest => dest.ProfilePath, opt => opt.Ignore())
                .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => src.IsActive ?? true));
        }
    }
}
