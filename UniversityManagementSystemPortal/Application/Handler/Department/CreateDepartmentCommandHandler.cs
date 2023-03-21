using AutoMapper;
using MediatR;
using UniversityManagementsystem.Models;
using UniversityManagementSystemPortal.Application.Command.Department;
using UniversityManagementSystemPortal.Authorization;
using UniversityManagementSystemPortal.IdentityServices;
using UniversityManagementSystemPortal.Interfaces;
using UniversityManagementSystemPortal.ModelDto.Department;

namespace UniversityManagementSystemPortal
{
    public class CreateDepartmentCommandHandler : IRequestHandler<CreateDepartmentCommand, DepartmentCreateDto>
{
    private readonly IDepartmentRepository _departmentRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<CreateDepartmentCommandHandler> _logger;
    private readonly IIdentityServices _identityService;

    public CreateDepartmentCommandHandler(IDepartmentRepository departmentRepository, IMapper mapper, ILogger<CreateDepartmentCommandHandler> logger, IIdentityServices identityService)
    {
        _departmentRepository = departmentRepository;
        _mapper = mapper;
        _logger = logger;
        _identityService = identityService;
    }

    public async Task<DepartmentCreateDto> Handle(CreateDepartmentCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var existingDepartment = await _departmentRepository.GetDepartmentByNameAsync(request.Name);
            if (existingDepartment != null)
            {
                throw new AppException("Department with the same name already exists.");
            }

            var department = _mapper.Map<Department>(request);
            department.CreatedBy = _identityService.GetUserId();
            department.UpdatedBy = _identityService.GetUserId();

            department = await _departmentRepository.CreateDepartmentAsync(department);

            var departmentDto = _mapper.Map<DepartmentCreateDto>(department);
            _logger.LogInformation(" Successfully Created department ");

            return departmentDto;
        }
        catch (ArgumentException ex)
        {
            _logger.LogWarning("Failed to create department: {ErrorMessage}", ex.Message);
            throw new ArgumentException(ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while creating department");
            throw new Exception("An error occurred while creating department", ex);
        }
    }
}


}
