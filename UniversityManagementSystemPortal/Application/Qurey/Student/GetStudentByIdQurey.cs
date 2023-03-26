using MediatR;
using UniversityManagementSystemPortal.ModelDto.UserDto;
using UniversityManagementSystemPortal.Models.ModelDto.Student;

namespace UniversityManagementSystemPortal.Application.Qurey.Student
{
    public record GetStudentByIdQurey(Guid Id) : IRequest<StudentDto>;
}

