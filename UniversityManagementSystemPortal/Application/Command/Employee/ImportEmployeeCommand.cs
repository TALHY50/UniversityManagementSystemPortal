using MediatR;
using UniversityManagementSystemPortal.ModelDto.Employee;
using UniversityManagementSystemPortal.ModelDto.Student;

namespace UniversityManagementSystemPortal.Application.Command.Employee
{
    public class ImportEmployeeCommand : IRequest
    {
        public IEnumerable<EmployeeReadModel> EmployeeData { get; }

        public ImportEmployeeCommand(IEnumerable<EmployeeReadModel> employeeData)
        {
            EmployeeData = employeeData;
        }
    }
}
