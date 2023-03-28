using AutoMapper;
using UniversityManagementSystemPortal.ModelDto.Category;

namespace UniversityManagementSystemPortal
{
    public class CategoryProfiler : Profile
    {
        public CategoryProfiler()
        {
            CreateMap<Category, CategoryDto>()
               .ForMember(dest => dest.InstituteName, opt => opt.MapFrom(src => src.Name));
            //.ForMember(dest => dest.Positions, opt => opt.MapFrom(src => src.Positions));

            CreateMap<CategoryCreateDto, Category>();

            CreateMap<CategoryUpdateDto, Category>();
        }
    }
}
