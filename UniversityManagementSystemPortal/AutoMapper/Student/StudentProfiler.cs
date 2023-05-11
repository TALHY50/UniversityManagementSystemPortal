using AutoMapper;
using UniversityManagementSystemPortal.Application.Command.Student;
using UniversityManagementSystemPortal.Helpers.Paging;
using UniversityManagementSystemPortal.ModelDto.StudentProgram;
using UniversityManagementSystemPortal.Models.ModelDto.Student;
using UniversityManagementSystemPortal.Models.ModelDto.StudentProgram;
using UniversityManagementSystemPortal.Models.ModelDto.UserDto;

namespace UniversityManagementSystemPortal
{
    public class StudentProfiler : Profile
    {
        public StudentProfiler()
        {

            CreateMap<Student, StudentDto>()
           .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.UserId ?? src.Id))
           .ForMember(dest => dest.AdmissionNo, opt => opt.MapFrom(src => src.AdmissionNo))
           .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address))
           .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category))
           .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address))
           .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.User.FirstName))
           .ForMember(dest => dest.MiddleName, opt => opt.MapFrom(src => src.User.MiddleName))
           .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.User.LastName))
           .ForMember(dest => dest.MobileNo, opt => opt.MapFrom(src => src.User.MobileNo))
           .ForMember(dest => dest.DateOfBirth, opt => opt.MapFrom(src => src.User.DateOfBirth))
           .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.User.Gender))
           .ForMember(dest => dest.BloodGroup, opt => opt.MapFrom(src => src.User.BloodGroup))
           .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.User.Email))
           .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => src.User.IsActive))
           .ForMember(dest => dest.SectionName, opt => opt.MapFrom(src => src.StudentPrograms.Select(sp => sp.Program.SectionName).FirstOrDefault()))
           .ForMember(dest => dest.ProgramName, opt => opt.MapFrom(src => src.StudentPrograms.Select(sp => sp.Program.Name).FirstOrDefault()))
           .ForMember(dest => dest.RoleNo, opt => opt.MapFrom(src => src.StudentPrograms.Select(sp => sp.RoleNo).FirstOrDefault()))
           .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.Username))
           .ForMember(dest => dest.EmailConfirm, opt => opt.MapFrom(src => src.User.EmailConfirmed));
            CreateMap<Student, StudentReadModel>();
            CreateMap<AddStudentCommand, Student>()
              .ForMember(dest => dest.Id, opt => opt.Ignore())
              .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => true));
            CreateMap<AddStudentDto, Student>()
             .ForMember(dest => dest.Id, opt => opt.Ignore())
             .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => true));
            //CreateMap<Student, AddStudentDto>();

            CreateMap<UpdateStudentDto, Student>();
            CreateMap<UpdateStudentCommand, Student>()
           .ForMember(dest => dest.Id, opt => opt.Ignore())
           .ForMember(dest => dest.Institute, opt => opt.Ignore())
           .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
           .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore());
            CreateMap<StudentReadModel, Student>()
           .ForMember(dest => dest.AdmissionNo, opt => opt.MapFrom(src => src.AdmissionNo))
           .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category))
           .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address));

            CreateMap<StudentProgramReadDto, StudentProgram>()
                .ForMember(dest => dest.RoleNo, opt => opt.MapFrom(src => src.RoleNo))
                .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => src.IsActive))
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.UtcNow));

            CreateMap<UserViewModel, User>()
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                     .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
           .ForMember(dest => dest.MiddleName, opt => opt.MapFrom(src => src.MiddleName))
           .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
           .ForMember(dest => dest.MobileNo, opt => opt.MapFrom(src => src.MobileNo))
           .ForMember(dest => dest.DateOfBirth, opt => opt.MapFrom(src => src.DateOfBirth))
           .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.Gender))
                .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.Username))
                .ForMember(dest => dest.EmailConfirmed, opt => opt.MapFrom(src => src.EmailConfirmed))
             .ForMember(dest => dest.BloodGroup, opt => opt.MapFrom(src => src.BloodGroup));


            CreateMap<PaginatedList<Student>, PaginatedList<StudentDto>>();
        }
    }
}