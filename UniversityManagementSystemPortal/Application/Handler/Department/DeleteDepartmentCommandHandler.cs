using MediatR;
using UniversityManagementSystemPortal.Application.Command.Department;
using UniversityManagementSystemPortal.Authorization;
using UniversityManagementSystemPortal.Interfaces;

namespace UniversityManagementSystemPortal.Application.Handler.Department
{
    public class DeleteDepartmentCommandHandler : IRequestHandler<DeleteDepartmentCommand>
    {
        private readonly IDepartmentRepository _departmentRepository;

        public DeleteDepartmentCommandHandler(IDepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }

        public async Task<Unit> Handle(DeleteDepartmentCommand request, CancellationToken cancellationToken)
        {
            var department = await _departmentRepository.GetDepartmentByIdAsync(request.DepartmentId);
            if (department == null)
            {
                throw new AppException("Department not found.");
            }

            await _departmentRepository.DeleteDepartmentAsync(request.DepartmentId);
            return Unit.Value;
        }
    }
}
