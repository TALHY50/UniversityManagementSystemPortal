using MediatR;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using UniversityManagementsystem.Models;
using UniversityManagementSystemPortal.Application.Command.Employee;
using UniversityManagementSystemPortal.IdentityServices;
using UniversityManagementSystemPortal.Interfaces;
using UniversityManagementSystemPortal.Interfce;
using UniversityManagementSystemPortal.Repository;

namespace UniversityManagementSystemPortal
{
    public class ImportEmployeeCommandHandler : IRequestHandler<ImportEmployeeCommand>
    {
        private readonly IUserInterface _userInterface;
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IEmployeeRepository _employeeRepository ;
        private readonly ICategoryRepository _categoryRepository  ;
        private readonly IIdentityServices _identityServices;
        private readonly IInstituteAdminRepository  _instituteAdminRepository;


        private readonly IPositionRepository _positionRepository ;
        public ImportEmployeeCommandHandler(IUserInterface userInterface, IDepartmentRepository departmentRepository,
            IEmployeeRepository employeeRepository, IPositionRepository positionRepository, 
            ICategoryRepository categoryRepository, IIdentityServices identityServices,
 IInstituteAdminRepository instituteAdminRepository

            )
        {
            _userInterface = userInterface;
            _departmentRepository = departmentRepository;
            _employeeRepository = employeeRepository;
            _positionRepository = positionRepository;
            _categoryRepository = categoryRepository;
            _identityServices = identityServices;
            _instituteAdminRepository = instituteAdminRepository;
        }
        public async Task<Unit> Handle(ImportEmployeeCommand request, CancellationToken cancellationToken)
        {
            var activeInstituteId = await _instituteAdminRepository.GetInstituteIdByActiveUserId(_identityServices.GetUserId().Value);
            foreach (var data in request.EmployeeData)
            {
                // Create user model from CSV data
                var userModel = new User
                {
                    Id = Guid.NewGuid(),
                    FirstName = data.FirstName,
                    MiddleName = data.MiddleName,
                    LastName = data.LastName,
                    MobileNo = data.MobileNo,
                    DateOfBirth = data.DateOfBirth,
                    Gender = data.Gender,
                    Email = data.Email,
                    BloodGroup = data.BloodGroup,
                    Username = data.Username,
                    Password = data.Password,
                    IsActive = data.IsActive,

                    CreatedBy = _identityServices.GetUserId().Value,
                    UpdatedBy = _identityServices.GetUserId().Value
                    // Add other properties as needed
                };

                // Save user model to database
                await _userInterface.RegisterAsUser(userModel);

                // Retrieve department from database
                var department = await _departmentRepository.GetDepartmentByNameAsync(data.DepartmentName);

                // Retrieve position from database
                var position = await _positionRepository.GetPositionByName(data.PositionName);
                // Create employee model from CSV data
                var employeeModel = new Employee
                {
                    Id = Guid.NewGuid(),
                    EmployeeNo = data.EmployeeNo,
                    EmployeeType = data.EmployeeType,
                    Address = data.EmployeAddress,
                    JoiningDate = data.JoiningDate,
                    DepartmentId = department.Id,
                    PositionId = position.Id,
                    UserId = userModel.Id,
                    InstituteId = activeInstituteId.Value,
                    CreatedBy = _identityServices.GetUserId().Value,
                    UpdatedBy = _identityServices.GetUserId().Value
                };

                // Save employee model to database
                await _employeeRepository.AddAsync(employeeModel);
            }
            return Unit.Value;
        }

    }
}
