
using AutoMapper;
using UniversityManagementsystem.Models;
using UniversityManagementSystemPortal.ModelDto.Student;
using UniversityManagementSystemPortal.ModelDto.StudentProgram;

namespace UniversityManagementSystemPortal
{
    public class StudentProgramProfiler : Profile
    {
        public StudentProgramProfiler()
        {
            CreateMap<StudentProgram, StudentProgramDto>();
            CreateMap<StudentProgramCreateDto, StudentProgram>();
            CreateMap<StudentProgramUpdateDto, StudentProgram>();

        }

    }
}
