﻿using AutoMapper;
using MediatR;
using UniversityManagementSystemPortal.Application.Qurey.Department;
using UniversityManagementSystemPortal.Interfaces;
using UniversityManagementSystemPortal.ModelDto.Department;

namespace UniversityManagementSystemPortal.Application.Handler.Department
{
    public class GetAllDepartmentsQueryHandler : IRequestHandler<GetAllDepartmentsQuery, IEnumerable<DepartmentDto>>
    {
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IMapper _mapper;

        public GetAllDepartmentsQueryHandler(IDepartmentRepository departmentRepository, IMapper mapper)
        {
            _departmentRepository = departmentRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<DepartmentDto>> Handle(GetAllDepartmentsQuery request, CancellationToken cancellationToken)
        {
            var departments = await _departmentRepository.GetAllDepartmentsAsync();
            var departmentDtos = _mapper.Map<IEnumerable<DepartmentDto>>(departments);
            return departmentDtos;
        }
    }

}