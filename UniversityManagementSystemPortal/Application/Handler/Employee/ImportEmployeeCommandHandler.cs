using MediatR;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using UniversityManagementSystemPortal.Application.Command.Employee;
using UniversityManagementSystemPortal.IdentityServices;
using UniversityManagementSystemPortal.Interfaces;
using UniversityManagementSystemPortal.Repository;

namespace UniversityManagementSystemPortal
{
    public class ImportEmployeeCommandHandler : IRequestHandler<ImportEmployeeCommand, List<string>>
    {
        private readonly IUserInterface _userInterface;
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IIdentityServices _identityServices;
        private readonly IInstituteAdminRepository _instituteAdminRepository;


        private readonly IPositionRepository _positionRepository;
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
        public async Task<List<string>> Handle(ImportEmployeeCommand request, CancellationToken cancellationToken)
        {
            var skippedEntries = await _employeeRepository.Upload(request.EmployeeData);
            return skippedEntries;
        }

    }
}
