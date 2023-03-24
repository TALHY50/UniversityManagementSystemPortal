﻿using MediatR;
using UniversityManagementSystemPortal.ModelDto.Department;

namespace UniversityManagementSystemPortal.Application.Qurey.Department
{
    public class GetDepartmentsByInstituteIdQuery : IRequest<IEnumerable<DepartmentDto>>
    {
        public Guid InstituteId { get; set; }
    }
}