using MediatR;
using UniversityManagementSystemPortal.Enum;
using UniversityManagementSystemPortal.Models.ModelDto.Student;

namespace UniversityManagementSystemPortal.Application.Command.Student
{
    public record AddStudentCommand(AddStudentDto addStudentDto): IRequest<AddStudentDto>;

}
