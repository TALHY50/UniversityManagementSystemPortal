using MediatR;
using UniversityManagementSystemPortal.Helpers.Paging;

namespace UniversityManagementSystemPortal.Application.Qurey.Student
{
    public class GetStudentListQuery : IRequest<PaginatedList<StudentDto>>
    {
        public PaginatedViewModel PaginatedViewModel { get; set; }
    }
}
