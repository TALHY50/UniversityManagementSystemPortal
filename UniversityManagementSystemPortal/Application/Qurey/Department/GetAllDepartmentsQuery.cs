﻿using MediatR;
using UniversityManagementSystemPortal.ModelDto.Department;

namespace UniversityManagementSystemPortal.Application.Qurey.Department
{
    public class GetAllDepartmentsQuery : IRequest<IEnumerable<DepartmentDto>>
    {
    }
}