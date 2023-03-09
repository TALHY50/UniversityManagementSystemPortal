using AutoMapper;
using UniversityManagementsystem.Models;
using UniversityManagementSystemPortal.ModelDto.Category;

namespace UniversityManagementSystemPortal
{
    public class CategoryProfiler : Profile
    {
        public CategoryProfiler() {
            CreateMap<Category, CategoryDto>()
               .ForMember(dest => dest.Institute, opt => opt.MapFrom(src => src.Institute));
               //.ForMember(dest => dest.Positions, opt => opt.MapFrom(src => src.Positions));

            CreateMap<CategoryCreateDto, Category>();

            CreateMap<CategoryUpdateDto, Category>();
        }
    }
}
