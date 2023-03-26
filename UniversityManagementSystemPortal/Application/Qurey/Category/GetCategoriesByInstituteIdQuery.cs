using MediatR;
namespace UniversityManagementSystemPortal.Application.Qurey.Category
{
    public class GetCategoriesByInstituteIdQuery : IRequest<List<CategoryDto>>
    {
        public Guid InstituteId { get; set; }
    }
}
