using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using UniversityManagementSystemPortal.Application.Command.Employee;
using UniversityManagementSystemPortal.Authorization;
using UniversityManagementSystemPortal.IdentityServices;
using UniversityManagementSystemPortal.Interfaces;
using UniversityManagementSystemPortal.ModelDto.Employee;
using UniversityManagementSystemPortal.PictureManager;

namespace UniversityManagementSystemPortal
{
    public class AddEmployeeCommandHandler : IRequestHandler<CreateEmployeeCommand, CreateEmployeeDto>
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;
        private readonly IPictureManager _pictureManager;
        private readonly IIdentityServices _identityServices;
        private readonly ILogger<AddEmployeeCommandHandler> _logger;
        public AddEmployeeCommandHandler(
            IEmployeeRepository employeeRepository,
            IMapper mapper,
            IPictureManager pictureManager,
            IIdentityServices identityServices,
            ILogger<AddEmployeeCommandHandler> logger)
        {
            _employeeRepository = employeeRepository;
            _mapper = mapper;
            _pictureManager = pictureManager;
            _identityServices = identityServices;
            _logger = logger;
        }

        public async Task<CreateEmployeeDto> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var employeeNoExists = await _employeeRepository.EmployeeNoExistsAsync(request.createEmployeeDto.EmployeeNo);

                if (employeeNoExists)
                {
                    throw new AppException($"Employee with employee number {request.createEmployeeDto.EmployeeNo} already exists.");
                }

                var userId = _identityServices.GetUserId();

                if (userId == null)
                {
                    throw new UnauthorizedAccessException("You must be logged in to perform this action.");
                }

                var employee = _mapper.Map<Employee>(request.createEmployeeDto);
                employee.CreatedBy = userId.Value;

                if (request.createEmployeeDto.Picture != null)
                {
                    var fileName = await _pictureManager.Upload(request.createEmployeeDto.Picture);
                    employee.ProfilePath = fileName;
                }

                await _employeeRepository.AddAsync(employee);

                var employeeDto = _mapper.Map<CreateEmployeeDto>(employee);

                return employeeDto;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating an employee");
                throw;
            }
        }



    }


}
