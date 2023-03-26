﻿using AutoMapper;
using UniversityManagementSystemPortal.ModelDto.Institute;
using UniversityManagementSystemPortal.ModelDto.InstituteDto;
using UniversityManagementSystemPortal.ModelDto.Role;
using UniversityManagementSystemPortal.Models.ModelDto.Role;

namespace UniversityManagementSystemPortal
{
    public class RoleMapper : Profile
    {
        public RoleMapper()
        {

            CreateMap<Role, RoleDto>();
            CreateMap<AddRoleDto, Role>();
            CreateMap<UpdateRoleDto, Role>();
        }
    }
}
