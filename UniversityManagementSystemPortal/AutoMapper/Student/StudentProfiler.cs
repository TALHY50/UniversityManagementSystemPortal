using AutoMapper;
using UniversityManagementsystem.Models;
using UniversityManagementSystemPortal.ModelDto.Student;

namespace UniversityManagementSystemPortal
{
    public class StudentProfiler : Profile
    {
        public StudentProfiler() {
            CreateMap<Student, StudentDto>();
            //.ForMember(dest => dest.FullName, opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}"))
            //.ForMember(dest => dest.InstituteName, opt => opt.MapFrom(src => src.Institute.Name))
            //.ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => ((Category)src.Category).ToString()))
            //.ReverseMap();

            CreateMap<AddStudentDto, Student>();
            //.ForMember(dest => dest.Id, opt => opt.MapFrom(_ => Guid.NewGuid()))
            //.ForMember(dest => dest.Category, opt => opt.MapFrom(src => (Category)src.Category))
            //.ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(_ => DateTime.UtcNow))
            //.ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.CreatedBy.GetValueOrDefault()))
            //.ForMember(dest => dest.IsActive, opt => opt.MapFrom(_ => true))
            //.ForMember(dest => dest.InstituteId, opt => opt.MapFrom(src => src.InstituteId))
            //.ReverseMap();

            CreateMap<UpdateStudentDto, Student>();
        }
    }
}
