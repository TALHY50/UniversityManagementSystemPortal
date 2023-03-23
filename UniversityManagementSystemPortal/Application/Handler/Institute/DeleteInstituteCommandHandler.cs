using MediatR;
using UniversityManagementSystemPortal.Application.Command.Institute;
using UniversityManagementSystemPortal.Authorization;
using UniversityManagementSystemPortal.Interfaces;

namespace UniversityManagementSystemPortal.Application.Handler.Institute
{
    public class DeleteInstituteCommandHandler : IRequestHandler<DeleteInstituteCommand>
    {
        private readonly IInstituteRepository _repository;

        public DeleteInstituteCommandHandler(IInstituteRepository repository)
        {
            _repository = repository;
        }

        public async Task<Unit> Handle(DeleteInstituteCommand request, CancellationToken cancellationToken)
        {
            var institute = await _repository.GetByIdAsync(request.Id);

            if (institute == null)
            {
                throw new AppException(nameof(Institute), request.Id);
            }

            await _repository.DeleteAsync(request.Id);

            return Unit.Value;
        }
    }
}
