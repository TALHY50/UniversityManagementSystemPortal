using MediatR;

namespace UniversityManagementSystemPortal.Application.Command.Category
{
    public class DeleteCategoryCommand : IRequest
    {
        public Guid Id { get; set; }
    }
}
