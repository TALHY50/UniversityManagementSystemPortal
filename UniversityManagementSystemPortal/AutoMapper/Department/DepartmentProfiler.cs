using AutoMapper;
using UniversityManagementsystem.Models;
using UniversityManagementSystemPortal.ModelDto.Department;

namespace UniversityManagementSystemPortal.AutoMapper
{
    public class DepartmentProfiler : Profile
    {
        public DepartmentProfiler() 
        {
            CreateMap<Department, DepartmentDto>();
            CreateMap<DepartmentCreateDto, Department>();
            CreateMap<DepartmentUpdateDto, Department>();
        }
    }
}
