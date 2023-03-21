using AutoMapper;
using MediatR;
using UniversityManagementSystemPortal.Application.Command.Employee;
using UniversityManagementSystemPortal.Authorization;
using UniversityManagementSystemPortal.IdentityServices;
using UniversityManagementSystemPortal.Interfaces;
using UniversityManagementSystemPortal.ModelDto.Employee;
using UniversityManagementSystemPortal.PictureManager;

namespace UniversityManagementSystemPortal.Application.Handler.Employee
{
    public class UpdateEmployeeHandler : IRequestHandler<UpdateEmployeeCommand, UpdateEmployeeDto>
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;
        private readonly IPictureManager _pictureManager;
        private readonly IIdentityServices _identityServices;
        private readonly ILogger<UpdateEmployeeHandler> _logger;

        public UpdateEmployeeHandler(
            IEmployeeRepository employeeRepository,
            IMapper mapper,
            IPictureManager pictureManager,
            IIdentityServices identityServices,
            ILogger<UpdateEmployeeHandler> logger)
        {
            _employeeRepository = employeeRepository;
            _mapper = mapper;
            _pictureManager = pictureManager;
            _identityServices = identityServices;
            _logger = logger;
        }

        public async Task<UpdateEmployeeDto> Handle(UpdateEmployeeCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var userId = _identityServices.GetUserId();
                if (userId == null)
                {
                    throw new UnauthorizedAccessException("You must be logged in to perform this action.");
                }

                var employee = await _employeeRepository.GetByIdAsync(request.Id);
                if (employee == null)
                {
                    throw new AppException($"Employee with id {request.Id} not found.");
                }

                _mapper.Map(request, employee);
                employee.UpdatedBy = userId.Value;

                if (request.ProfilePicture != null)
                {
                    var fileName = await _pictureManager.Update(request.Id, request.ProfilePicture, employee.ProfilePath);
                    employee.ProfilePath = fileName;
                }

                await _employeeRepository.UpdateAsync(employee);

                var employeeDto = _mapper.Map<UpdateEmployeeDto>(employee);

                return employeeDto;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating an employee");
                throw;
            }
        }
    }
}
