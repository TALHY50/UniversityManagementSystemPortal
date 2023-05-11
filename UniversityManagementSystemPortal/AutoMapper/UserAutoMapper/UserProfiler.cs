using AutoMapper;
using UniversityManagementSystemPortal.Application.Command.Account;
using UniversityManagementSystemPortal.Helpers.Paging;
using UniversityManagementSystemPortal.ModelDto.UserDto;
using UniversityManagementSystemPortal.Models.ModelDto.UserDto;

namespace UniversityManagementSystemPortal
{
    public class UserProfiler : Profile
    {
        public UserProfiler()
        {
            CreateMap<User, UserViewModel>();
            CreateMap<RegisterUserCommand, User>()
             .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.registorUserDto.FirstName))
    .ForMember(dest => dest.MiddleName, opt => opt.MapFrom(src => src.registorUserDto.MiddleName))
    .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.registorUserDto.LastName))
    .ForMember(dest => dest.BloodGroup, opt => opt.MapFrom(src => src.registorUserDto.BloodGroup))
    .ForMember(dest => dest.MobileNo, opt => opt.MapFrom(src => src.registorUserDto.MobileNo))
    .ForMember(dest => dest.DateOfBirth, opt => opt.MapFrom(src => src.registorUserDto.DateOfBirth))
    .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.registorUserDto.Gender))
    .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.registorUserDto.Email))
    .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.registorUserDto.Username))
    .ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.registorUserDto.Password));
            CreateMap<User, RegistorUserDto>();
            CreateMap<RegistorUserDto, User>();
            CreateMap<User, UpdateUserDto>();
            CreateMap<PaginatedList<User>, PaginatedList<UserViewModel>>();
        }
    }
}
