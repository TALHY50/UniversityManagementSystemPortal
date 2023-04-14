using AutoMapper;
using UniversityManagementSystemPortal.Application.Command.Institute;
using UniversityManagementSystemPortal.ModelDto.Institute;
using UniversityManagementSystemPortal.ModelDto.InstituteDto;
using UniversityManagementSystemPortal.Models.ModelDto.Institute;

namespace UniversityManagementSystemPortal.AutoMapper
{
    public class InstituteProilfer : Profile
    {
        public InstituteProilfer()
        {
            CreateMap<Institute, InstituteDto>();
            CreateMap<InstituteCreateDto, Institute>();
            CreateMap<InstituteUpdateDto, Institute>();
            CreateMap<InstituteUpdateDto, InstituteUpdateCommand>();
            CreateMap<InstituteUpdateCommand, Institute>();
            CreateMap<Institute, InstituteUpdateDto>();
        }
    }
}
