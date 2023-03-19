using MediatR;
using UniversityManagementSystemPortal.ModelDto.UserDto;

namespace UniversityManagementSystemPortal.Application.Qurey.Student
{
    public record GetStudentByIdQurey(Guid Id) : IRequest<StudentDto>;
}

