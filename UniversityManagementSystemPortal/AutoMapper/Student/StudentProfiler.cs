using AutoMapper;
using UniversityManagementsystem.Models;
using PorgramNamespace = UniversityManagementsystem.Models.Program;
using UniversityManagementSystemPortal.ModelDto.Student;

namespace UniversityManagementSystemPortal
{
    public class StudentProfiler : Profile
    {
        public StudentProfiler()
        {
            CreateMap<Student, StudentDto>();
            CreateMap<Student, StudentReadModel>();
            CreateMap<AddStudentDto, Student>();
            CreateMap<UpdateStudentDto, Student>();
            CreateMap<StudentReadModel, Student>()
        .ForMember(dest => dest.AdmissionNo, opt => opt.MapFrom(src => src.AdmissionNo))
        .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category.ToString()))
        .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address))
        .ForMember(dest => dest.InstituteId, opt => opt.Ignore())
        .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => src.IsActive));

            CreateMap<StudentReadModel, User>()
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
                .ForMember(dest => dest.MiddleName, opt => opt.MapFrom(src => src.MiddleName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
                .ForMember(dest => dest.MobileNo, opt => opt.MapFrom(src => src.MobileNo))
                .ForMember(dest => dest.DateOfBirth, opt => opt.MapFrom(src => src.DateOfBirth))
                .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.Gender.ToString()))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.EmailConfirmed, opt => opt.MapFrom(src => src.EmailConfirm))
                .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.UserName))
                .ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.Password))
                .ForMember(dest => dest.BloodGroup, opt => opt.MapFrom(src => src.BloodGroup))
                .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => src.IsActive));

            CreateMap<StudentReadModel, StudentProgram>()
                .ForMember(dest => dest.StudentId, opt => opt.Ignore())
                .ForMember(dest => dest.ProgramId, opt => opt.Ignore())
                .ForMember(dest => dest.RoleNo, opt => opt.MapFrom(src => src.RoleNo))
                .ForPath(dest => dest.Program, opt => opt.MapFrom(src => src.ProgramName))
                .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => src.IsActive))
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedBy, opt => opt.Ignore());
            CreateMap<StudentReadModel, StudentDto>();
        }
    }
}
