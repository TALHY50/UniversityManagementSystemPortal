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

            CreateMap<PorgramNamespace, ProgramReadDto>();
            CreateMap<ProgramCreateDto, PorgramNamespace>();
            CreateMap<ProgramUpdateDto, PorgramNamespace>();
        }
    }
}
