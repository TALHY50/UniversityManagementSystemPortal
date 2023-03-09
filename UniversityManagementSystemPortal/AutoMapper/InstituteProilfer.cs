using AutoMapper;
using UniversityManagementsystem.Models;
using UniversityManagementSystemPortal.ModelDto.Institute;
using UniversityManagementSystemPortal.ModelDto.InstituteDto;

namespace UniversityManagementSystemPortal.AutoMapper
{
    public class InstituteProilfer: Profile
    {
        public InstituteProilfer() {
            CreateMap<Institute, InstituteDto>();
            CreateMap<InstituteCreateDto, Institute>();
            CreateMap<InstituteUpdateDto, Institute>();
        }
    }
}
