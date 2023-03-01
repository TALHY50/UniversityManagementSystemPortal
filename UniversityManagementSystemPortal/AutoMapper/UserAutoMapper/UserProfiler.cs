using AutoMapper;
using UniversityManagementsystem.Models;
using UniversityManagementSystemPortal.ModelDto.UserDto;

namespace UniversityManagementSystemPortal.AutoMapper.UserAutoMapper
{
    public class UserProfiler : Profile
    {
        public UserProfiler()
        {
            CreateMap<User, RegistorViewModel>();
        }
    }
}
