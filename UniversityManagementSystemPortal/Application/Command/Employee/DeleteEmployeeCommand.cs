using MediatR;

namespace UniversityManagementSystemPortal.Application.Command.Employee
{
    public class DeleteEmployeeCommand : IRequest
    {
        public Guid Id { get; set; }
    }
}
