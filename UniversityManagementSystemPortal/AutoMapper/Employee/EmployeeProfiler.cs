using AutoMapper;
using UniversityManagementsystem.Models;
using UniversityManagementSystemPortal.Enum;
using UniversityManagementSystemPortal.ModelDto.Employee;

namespace UniversityManagementSystemPortal
{
    public class EmployeeProfiler : Profile
    {
        public EmployeeProfiler() {
            CreateMap<Employee, EmployeeDto>()
           .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.User.FirstName))
           .ForMember(dest => dest.MiddleName, opt => opt.MapFrom(src => src.User.MiddleName))
           .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.User.LastName))
           .ForMember(dest => dest.MobileNo, opt => opt.MapFrom(src => src.User.MobileNo))
           .ForMember(dest => dest.DateOfBirth, opt => opt.MapFrom(src => src.User.DateOfBirth))
           .ForMember(dest => dest.BloodGroup, opt => opt.MapFrom(src => (int?)src.User.BloodGroup))
           .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.User.Gender))
           .ForMember(dest => dest.EmployeeNo, opt => opt.MapFrom(src => src.EmployeeNo))
           .ForMember(dest => dest.EmployeeType, opt => opt.MapFrom(src => (EmployeeType)src.EmployeeType))
           .ForMember(dest => dest.EmployeAddress, opt => opt.MapFrom(src => src.Address))
           .ForMember(dest => dest.JoiningDate, opt => opt.MapFrom(src => src.JoiningDate))
           .ForMember(dest => dest.InstituteName, opt => opt.MapFrom(src => src.Institute.Name))
           .ForMember(dest => dest.ProfilePath, opt => opt.MapFrom(src => src.ProfilePath))
           .ForMember(dest => dest.DepartmentName, opt => opt.MapFrom(src => src.Department.Name))
           .ForMember(dest => dest.DepartmentCode, opt => opt.MapFrom(src => src.Department.Code))
           .ForMember(dest => dest.PositionName, opt => opt.MapFrom(src => src.Position.Name))
           .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => src.IsActive));

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
