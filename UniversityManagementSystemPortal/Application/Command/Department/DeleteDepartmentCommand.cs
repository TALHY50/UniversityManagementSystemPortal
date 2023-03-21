using MediatR;

namespace UniversityManagementSystemPortal.Application.Command.Department
{
    public class DeleteDepartmentCommand : IRequest
    {
        public Guid DepartmentId { get; set; }
    }
}
