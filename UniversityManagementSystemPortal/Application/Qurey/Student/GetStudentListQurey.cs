using MediatR;
using UniversityManagementSystemPortal.Helpers.Paging;
using UniversityManagementSystemPortal.Models.ModelDto.Student;

namespace UniversityManagementSystemPortal.Application.Qurey.Student
{
    public class GetStudentListQuery : IRequest<PaginatedList<StudentDto>>
    {
        public PaginatedViewModel? paginatedViewModel { get; set; }
    }
}
