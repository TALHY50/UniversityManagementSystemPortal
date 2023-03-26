using AutoMapper;
using UniversityManagementSystemPortal.ModelDto.InstituteAdmin;
using UniversityManagementSystemPortal.Models.ModelDto.InstituteAdmin;

namespace UniversityManagementSystemPortal
{
    public class InstituteAdminProfiler : Profile
    {
        public InstituteAdminProfiler()
        {
            CreateMap<InstituteAdmin, InstituteAdminDTO>();
            CreateMap<InstituteAdminCreateDto, InstituteAdmin>();
            CreateMap<InstituteAdminUpdateDto, InstituteAdmin>();
        }

    }
}
