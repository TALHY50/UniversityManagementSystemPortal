using AutoMapper;
using UniversityManagementsystem.Models;
using UniversityManagementSystemPortal.ModelDto.Institute;
using UniversityManagementSystemPortal.ModelDto.InstituteDto;
using UniversityManagementSystemPortal.ModelDto.Role;

namespace UniversityManagementSystemPortal
{
    public class RoleMapper : Profile
    {
        public RoleMapper() {

            CreateMap<Role, RoleDto>();
            CreateMap<CreateRoleDto, Role>();
            CreateMap<UpdateRoleDto, Role>();
        }
    }
}
