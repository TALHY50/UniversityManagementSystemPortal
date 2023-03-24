using MediatR;
using NuGet.Protocol.Core.Types;
using UniversityManagementSystemPortal.Application.Command.Category;
using UniversityManagementSystemPortal.Authorization;
using UniversityManagementSystemPortal.Interfaces;

namespace UniversityManagementSystemPortal.Application.Handler.Category
{
    public class DeleteCategoryHandler : IRequestHandler<DeleteCategoryCommand, Unit>
    {
        private readonly ICategoryRepository _repository;

        public DeleteCategoryHandler(ICategoryRepository repository)
        {
            _repository = repository;
        }

        public async Task<Unit> Handle(DeleteCategoryCommand command, CancellationToken cancellationToken)
        {
            var category = await _repository.GetByIdAsync(command.Id);

            if (category == null)
            {
                throw new AppException(nameof(Category), command.Id);
            }

            await _repository.DeleteAsync(category);
            return Unit.Value;
        }
    }
}
