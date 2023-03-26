using AutoMapper;
using UniversityManagementSystemPortal.ModelDto.Department;
using PorgramNamespace = UniversityManagementSystemPortal.Program;
using UniversityManagementSystemPortal.ModelDto.StudentProgram;
using UniversityManagementSystemPortal.Models.ModelDto.Program;
using UniversityManagementSystemPortal.ModelDto.Program;

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
