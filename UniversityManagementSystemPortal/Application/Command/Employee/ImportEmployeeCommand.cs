using MediatR;
using UniversityManagementSystemPortal.ModelDto.Employee;
using UniversityManagementSystemPortal.ModelDto.Student;

namespace UniversityManagementSystemPortal.Application.Command.Employee
{
    public class ImportEmployeeCommand : IRequest<List<string>>
    {
        public List<EmployeeReadModel> EmployeeData { get; }

        public ImportEmployeeCommand(List<EmployeeReadModel> employeeData)
        {
            EmployeeData = employeeData;
        }
    }
}
