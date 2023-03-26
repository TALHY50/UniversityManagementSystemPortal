using AutoMapper;
using UniversityManagementSystemPortal.ModelDto.Department;
using UniversityManagementSystemPortal.Models.ModelDto.Department;

namespace UniversityManagementSystemPortal
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
