using MediatR;
using UniversityManagementSystemPortal.Application.Command.Employee;
using UniversityManagementSystemPortal.Interfaces;

namespace UniversityManagementSystemPortal.Application.Handler.Employee
{
    public class DeleteEmployeeCommandHandler : IRequestHandler<DeleteEmployeeCommand>
    {
        private readonly ILogger<DeleteEmployeeCommandHandler> _logger;
        private readonly IEmployeeRepository _employeeRepository;

        public DeleteEmployeeCommandHandler(ILogger<DeleteEmployeeCommandHandler> logger, IEmployeeRepository employeeRepository)
        {
            _logger = logger;
            _employeeRepository = employeeRepository;
        }

        public async Task<Unit> Handle(DeleteEmployeeCommand request, CancellationToken cancellationToken)
        {
            var employee = await _employeeRepository.GetByIdAsync(request.Id);

            if (employee != null)
            {
                _employeeRepository.DeleteAsync(request.Id);
                _logger.LogInformation($"Employee with ID {request.Id} deleted successfully");
            }
            else
            {
                _logger.LogWarning($"Employee with ID {request.Id} not found");
            }

            return Unit.Value;
        }
    }

}
