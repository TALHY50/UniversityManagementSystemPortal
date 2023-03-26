using MediatR;
using UniversityManagementSystemPortal.Application.Command.Program;
using UniversityManagementSystemPortal.Authorization;
using UniversityManagementSystemPortal.Interfaces;

namespace UniversityManagementSystemPortal.Application.Handler.Program
{
    public class DeleteProgramCommandHandler : IRequestHandler<DeleteProgramCommand>
    {
        private readonly IProgramRepository _repository;

        public DeleteProgramCommandHandler(IProgramRepository repository)
        {
            _repository = repository;
        }

        public async Task<Unit> Handle(DeleteProgramCommand request, CancellationToken cancellationToken)
        {
            var program = await _repository.GetByIdAsync(request.Id);
            if (program == null)
            {
                throw new AppException("Program not found.");
            }

            await _repository.DeleteAsync(request.Id);
            return Unit.Value;
        }
    }
}
