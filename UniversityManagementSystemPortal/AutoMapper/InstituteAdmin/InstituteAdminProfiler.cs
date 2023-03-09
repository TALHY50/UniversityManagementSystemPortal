using AutoMapper;
using UniversityManagementsystem.Models;
using UniversityManagementSystemPortal.ModelDto.InstituteAdmin;

namespace UniversityManagementSystemPortal.AutoMapper
{
    public class InstituteAdminProfiler :Profile
    {
        public InstituteAdminProfiler()
        {
            CreateMap<InstituteAdmin, InstituteAdminDTO>();
            CreateMap<InstituteAdminCreateDto, InstituteAdmin>();
            CreateMap<InstituteAdminUpdateDto, InstituteAdmin>();
        }

    }
}
