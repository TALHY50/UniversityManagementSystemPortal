using AutoMapper;
using UniversityManagementSystemPortal.ModelDto.UserRoleDto;
using UniversityManagementSystemPortal.Models.ModelDto;

namespace UniversityManagementSystemPortal.AutoMapper
{
    public class UserRoleProfiler : Profile
    {
        public UserRoleProfiler() {
            
                CreateMap<UserRole, UserRoleDto>()
                    .ForMember(dest => dest.Roletype, opt => opt.MapFrom(src => src.Role.Name))
                    .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.Username));

                CreateMap<CreateUserRoleDto, UserRole>();

                CreateMap<UserRoleDto, UserRole>();
            
        }
    }
}
