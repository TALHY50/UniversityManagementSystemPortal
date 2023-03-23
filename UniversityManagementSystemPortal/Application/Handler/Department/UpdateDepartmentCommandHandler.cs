using AutoMapper;
using MediatR;
using Serilog;
using UniversityManagementSystemPortal.Application.Command.Department;
using UniversityManagementSystemPortal.Authorization;
using UniversityManagementSystemPortal.IdentityServices;
using UniversityManagementSystemPortal.Interfaces;
using UniversityManagementSystemPortal.ModelDto.Department;

namespace UniversityManagementSystemPortal.Application.Handler.Department
{
    public class UpdateDepartmentCommandHandler : IRequestHandler<UpdateDepartmentCommand, DepartmentUpdateDto>
    {
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IMapper _mapper;
        private readonly IIdentityServices _identityService;

        public UpdateDepartmentCommandHandler(IDepartmentRepository departmentRepository, IMapper mapper, IIdentityServices identityService)
        {
            _departmentRepository = departmentRepository;
            _mapper = mapper;
            _identityService = identityService;
        }

        public async Task<DepartmentUpdateDto> Handle(UpdateDepartmentCommand request, CancellationToken cancellationToken)
        {
            var department = await _departmentRepository.GetDepartmentByIdAsync(request.Id);
            if (department == null)
            {
                throw new AppException(nameof(Department), request.Id);
            }

            department.UpdatedBy = _identityService.GetUserId();

            _mapper.Map(request, department);

            await _departmentRepository.UpdateDepartmentAsync(department);

            var departmentDto = _mapper.Map<DepartmentUpdateDto>(department);

            Log.Information("Department updated successfully. Id: {DepartmentId}", department.Id);

            return departmentDto;
        }
    }

}

