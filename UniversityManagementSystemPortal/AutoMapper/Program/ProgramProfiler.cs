using AutoMapper;
using UniversityManagementsystem.Models;
using UniversityManagementSystemPortal.ModelDto.Department;
using UniversityManagementSystemPortal.ModelDto.Program;
using PorgramNamespace = UniversityManagementsystem.Models.Program;
using UniversityManagementSystemPortal.ModelDto.StudentProgram;

namespace UniversityManagementSystemPortal.AutoMapper.Program
{
    public class ProgramProfiler : Profile
    {
        public ProgramProfiler() {
           
            CreateMap<PorgramNamespace, ProgramReadDto>()
            .ForMember(dest => dest.DepartmentCode, opt => opt.MapFrom(src => src.Department.Code))
            .ForMember(dest => dest.DepartmentName, opt => opt.MapFrom(src => src.Department.Name))
            .ForMember(dest => dest.InstituteName, opt => opt.MapFrom(src => src.Department.Institute.Name));
            CreateMap<ProgramCreateDto, PorgramNamespace>();
            CreateMap<ProgramUpdateDto, PorgramNamespace>();
        }
    }
}
